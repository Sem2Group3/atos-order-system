using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using WebApplication_Atos.BLL.Services;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class _testModel : PageModel
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly MySqlDataSource _dataSource;

        public List<Employee> Employees { get; private set; } = new List<Employee>();
        private List<string> phcs;
        public List<string> Strings;

        public List<bool> DatabaseChanges = new();

        public _testModel(MySqlDataSource dataSource, IEmployeeBLL employeeBLL)
        {
            _dataSource = dataSource;
            _employeeBLL = employeeBLL;

            phcs = new List<string>
            {
                Hasher.GenerateSaltAndHashArgon2("john2"),
                Hasher.GenerateSaltAndHashArgon2("wachtwoord123"),
                Hasher.GenerateSaltAndHashArgon2("helmaaa"),
                Hasher.GenerateSaltAndHashArgon2("test123")
            };
            Strings = new List<string>
            {
                $"john pw: john2, phc: {phcs[0]}, role: 'orders', validation check: {Hasher.ComparePasswordToPHC("john2", phcs[0])}",
                $"hans pw: wachtwoord123, phc: {phcs[1]}, role: 'finance,orders,produce,client', validation check: {Hasher.ComparePasswordToPHC("wachtwoord123", phcs[1])}",
                $"helma pw: helmaaa, phc: {phcs[2]}, role: 'finance', validation check: {Hasher.ComparePasswordToPHC("helmaaa", phcs[2])}",
                $"testy pw: test123, phc: {phcs[3]}, role: 'client', validation check: {Hasher.ComparePasswordToPHC("test123", phcs[3])}",
            };
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
