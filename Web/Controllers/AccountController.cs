using Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class AccountController(IAuthService authService) : Controller
{
    [HttpGet]
    public IActionResult Login() {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await authService.LoginAsync(model.Email, model.Password, model.RememberMe);

        if (result.Succeeded)
            return RedirectToAction("Index", "Dashboard");
        
        if (result.IsLockedOut)
            ModelState.AddModelError(string.Empty, "Your account is locked out.");
        else if (result.IsNotAllowed)
            ModelState.AddModelError(string.Empty, "You are not allowed to log in.");
        else if (result.RequiresTwoFactor)
            ModelState.AddModelError(string.Empty, "Two-factor authentication is required.");
        else
             ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await authService.RegisterAsync(model.Email, model.Password);

        if (result.Succeeded)
            return RedirectToAction("Login");
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(GetField(error.Code), error.Description);   
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    private string GetField(string code)
    {
        if (code.Contains("Password"))
            return "Password";

        if (code.Contains("Email") || code.Contains("UserName"))
            return "Email";

        return string.Empty;
    }
}