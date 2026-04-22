using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class DashboardController : Controller
{
    [Authorize(Policy = Permissions.Dashboard.Read)]
    public IActionResult Index()
    {
        return View();
    }
}