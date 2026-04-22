using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class OrderConfirmationPreviewModel : PageModel
    {
        private readonly IOrderFlagBLL _orderFlagBLL;
        private readonly IClientBLL _clientBLL;

        public OrderConfirmationPreviewModel(IOrderFlagBLL orderFlagBLL, IClientBLL clientBLL)
        {
            _orderFlagBLL = orderFlagBLL;
            _clientBLL = clientBLL;
        }

        public OrderFlag OrderFlag { get; set; }
        public List<OrderFlagRule> OrderFlagRules { get; set; }
        public Client CurrentClient { get; set; }

        public decimal BTWPercentage => 21m;
        public decimal NettoTotaal => (decimal)OrderFlagRules.Sum(r => r.Hoeveelheid * r.Prijs);
        public decimal BTWBedrag => NettoTotaal * (BTWPercentage / 100);
        public decimal Totaal => NettoTotaal + BTWBedrag;

        public IActionResult OnGet(int id)
        {
            OrderFlag = _orderFlagBLL.GetOrderFlagByID(id);
            if (OrderFlag == null) return NotFound();

            OrderFlagRules = _orderFlagBLL.GetOrderFlagRules(id);
            CurrentClient = _clientBLL.GetClientFromID(OrderFlag.KlantId);
            return Page();
        }
    }
}
