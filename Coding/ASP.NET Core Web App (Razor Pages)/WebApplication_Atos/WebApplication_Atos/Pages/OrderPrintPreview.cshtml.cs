using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class OrderPrintPreviewModel : PageModel
    {
        private readonly IOrderFlagBLL _orderFlagBLL;
        private readonly IClientBLL _clientBLL;

        public OrderPrintPreviewModel(IOrderFlagBLL orderFlagBLL, IClientBLL clientBLL)
        {
            _orderFlagBLL = orderFlagBLL;
            _clientBLL = clientBLL;
        }

        public OrderFlag OrderFlag { get; set; }
        public List<OrderFlagRule> OrderFlagRules { get; set; }
        public Client CurrentClient { get; set; }

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
