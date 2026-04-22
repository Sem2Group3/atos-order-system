using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class PrintConfectiebonModel : PageModel
    {
        private readonly IClientBLL _clientBLL;
        private readonly IOrderPrintedFlagBLL _orderBLL;

        public Client Client { get; set; }
        public PrintedFlag Order { get; set; }
        public PrintedFlagRule OrderRule { get; set; }

        public PrintConfectiebonModel(IClientBLL clientBLL, IOrderPrintedFlagBLL orderBLL)
        {
            _clientBLL = clientBLL;
            _orderBLL = orderBLL;
        }

        public void OnGet(int id)
        {
            Order = _orderBLL.GetPrintedFlag(id);
            OrderRule = _orderBLL.GetPrinteFlagRules(id);
            Client = _clientBLL.GetClientFromID(Order.KlantId);
        }
    }
}
