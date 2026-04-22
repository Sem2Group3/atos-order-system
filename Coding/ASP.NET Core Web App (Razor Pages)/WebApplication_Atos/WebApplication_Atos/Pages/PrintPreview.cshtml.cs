using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Text.Json;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;
using WebApplication_Atos.Interfaces;
using WebApplication_Atos.Models;

namespace WebApplication_Atos.Pages
{
    public class PrintPreviewModel : PageModel
    {
        private readonly IOrderBLL _orderBLL;

        public PrintPreviewModel(IOrderBLL orderBLL)
        {
            _orderBLL = orderBLL;
        }

        public Order Order { get; set; }
        public List<OrderRule> OrderRules { get; set; } = new();

        public decimal BTWPercentage => 21m;
        public decimal NettoTotaal => OrderRules.Sum(r => r.Hoeveelheid * r.Prijs);
        public decimal BTWBedrag => NettoTotaal * (BTWPercentage / 100);
        public decimal Totaal => NettoTotaal + BTWBedrag;

        public IActionResult OnGet(int id)
        {
            Order = _orderBLL.GetOrders().FirstOrDefault(o => o.PrijsopgaveId == id);
            if (Order == null) return NotFound();

            OrderRules = _orderBLL.GetOrderRules(id);
            return Page();
        }
    }
}
