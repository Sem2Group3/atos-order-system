using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Security.Claims;

namespace WebApplication_Atos.Pages
{
    public class Main_SwitchboardModel : PageModel
    {
        private readonly MySqlDataSource _dataSource;
        public Main_SwitchboardModel(MySqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public IActionResult OnGet()
        {
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

            return Page();
        }
    }
}
