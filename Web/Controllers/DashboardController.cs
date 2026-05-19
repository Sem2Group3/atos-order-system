using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class DashboardController : Controller
{
    [HttpGet]
    [Authorize(Policy = Permissions.Dashboard.View)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    // [Authorize(Policy = Permissions.Dashboard.View)]
    public IActionResult Customer()
    {
        var customer = new CustomerViewModel
        {
            CustomerId = 10245,
            Name = "Janssen Bouw B.V.",
            OutstandingAmount = 1845.75m,
            Address = "Marktstraat 12",
            PostalCode = "5611AB",
            City = "Eindhoven",
            PhoneNumber = "040-1234567",
            MobileNumber = "06-12345678",
            EmailAddress = "info@janssenbouw.nl",
            AttentionOf = "Dhr. P. Janssen",
            Notes = "Voorkeur voor communicatie per e-mail.",
            IsBlocked = false,

            InvoiceDetails = new InvoiceDetailsViewModel
            {
                Name = "Janssen Bouw B.V.",
                Address = "Factuurweg 5",
                PostalCode = "5612CD",
                City = "Eindhoven",
                InvoiceAttentionOf = "Financiële administratie",
                DigitalInvoice = "facturen@janssenbouw.nl",
                VatNumber = "NL123456789B01",
                ContactNotes = "Facturen maandelijks versturen."
            }
        };
        
        return View(customer);
    }

}