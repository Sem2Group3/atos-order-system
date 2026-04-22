using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using WebApplication_Atos.BLL.Services;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages;

public class IndexModel : PageModel
{
    private readonly IEmployeeBLL _employeeBLL;
    private readonly MySqlDataSource _dataSource;

    public List<Employee> Employees { get; private set; } = new List<Employee>();

    public IndexModel(MySqlDataSource dataSource, IEmployeeBLL  employeeBLL)
    {
        _dataSource = dataSource;
        _employeeBLL =  employeeBLL;
    }

    public IActionResult OnGet()
    {
        bool loggedIn = CheckIfLoggedIn();
        if (loggedIn) return new RedirectToPageResult("./Main-Switchboard");


        try
        {
            using var conn = _dataSource.CreateConnection();
            conn.Open();
        }
        catch
        {
            TempData["ErrorMessage"] = "Kan geen verbinding maken met de database.";
            return RedirectToPage("/Error");
        }

        Employees = _employeeBLL.GetEmployees();
        return Page();
    }

    public IActionResult OnPost(string usernameInput, string passwordInput)
    {
        if (!ValidateInput(usernameInput, passwordInput)) {
            return Page();
        }

        bool authenticated = AuthenticateUser(usernameInput, passwordInput);

        if (!authenticated)
        {
            ModelState.AddModelError("LoginError", "Invalid username or password.");

            return Page();
        }

        Employee? employee = _employeeBLL.GetEmployeeByUsername(usernameInput);

        if (employee != null)
        {
            StoreAuthorization(employee);
            return new RedirectToPageResult("./Main-Switchboard");
        }
        else
        {
            ModelState.AddModelError("LoginError", "An unknown error has occurred.");

            return Page();
        }
    }
    
    private bool CheckIfLoggedIn()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var idValue = User.FindFirst("ID")?.Value;
            if (int.TryParse(idValue, out int id))
            {
                return true;
            }
        }
        return false;
    }
    
    private bool ValidateInput(string usernameInput, string passwordInput)
    {
        if (string.IsNullOrEmpty(usernameInput))
        {
            ModelState.AddModelError("LoginError", "Username field cannot be empty.");
            return false;
        }

        if (string.IsNullOrEmpty(passwordInput))
        {
            ModelState.AddModelError("LoginError", "Password field cannot be empty.");
            return false;
        }

        return true;
    }

    private bool AuthenticateUser(string username, string password)
    {
        Authenticator _authenticator = new(_employeeBLL);
        return _authenticator.AuthenticateUser(username, password);
    }

    private void StoreAuthorization(Employee employee)
    {
        string role = employee.Role ?? "";

        List<Claim> claims = new();
        claims.Add(new Claim(ClaimTypes.Name, employee.Werknemer ?? ""));
        claims.Add(new Claim("ID", employee.WerknemerId.ToString()));

        if (role.Contains(","))
        {
            List<string> roles = role.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r.Trim()));
            }
        } else
        {
            claims.Add(new Claim(ClaimTypes.Role, employee.Role ?? ""));
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
    }
}
