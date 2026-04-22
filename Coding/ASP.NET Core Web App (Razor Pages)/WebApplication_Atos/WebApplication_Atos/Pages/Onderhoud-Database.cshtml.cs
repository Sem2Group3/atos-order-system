using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication_Atos.Pages
{
    public class Onderhou_DatabaseModel : PageModel
    {
        private bool IsAuthorized()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role) || !role.Contains("database")) return false;
            else return true;
        }

        public void OnGet()
        {
            if (!IsAuthorized())
            {
                Response.StatusCode = 403;
            }
        }
    }
}
