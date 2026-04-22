using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Interfaces;
using WebApplication_Atos.Models;
using WebApplication_Atos.Core.Models;
using System.Linq;
using System.Text.Json;
using WebApplication_Atos.BLL.BLL;
using MySqlConnector;

namespace WebApplication_Atos.Pages
{
    public class SubCustomerPageModel : PageModel
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IItemBLL _itemBLL;
        private readonly IOrderBLL _orderBLL;
        private readonly IClientBLL _clientBLL;
        private readonly MySqlDataSource _dataSource;
        public int CurrentClientId { get; set; }
        public Client CurrentClient { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        [BindProperty]
        public Order Order { get; set; } = new Order();
        [BindProperty]
        public List<OrderRule> OrderRules { get; set; } = new List<OrderRule>();
        [BindProperty]
        public List<OrderRule> NewOrderRules { get; set; } = new List<OrderRule>();
        public List<Order> Levertijd { get; set; }
        public List<Order> ReferentieText { get; set; }
        [BindProperty]
        public int BTW { get; set; } = 21;

        public SubCustomerPageModel(MySqlDataSource dataSource ,IEmployeeBLL employeeBLL, IItemBLL itemBLL, IOrderBLL orderBLL, IClientBLL clientBLL)
        {
            _employeeBLL = employeeBLL;
            _itemBLL = itemBLL;
            _orderBLL = orderBLL;
            Items = _itemBLL.GetItems();
            Employees = _employeeBLL.GetEmployees();
            _clientBLL = clientBLL;
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
            if (HttpContext.Session.GetInt32("clientID") == null)
            {
                return RedirectToPage("/CustomerPage");
            }
            CurrentClientId = (int)HttpContext.Session.GetInt32("clientID");
            CurrentClient = _clientBLL.GetClientFromID(CurrentClientId);
            int? orderID = HttpContext.Session.GetInt32("prisjogaveID");
            if (orderID != null)
            {
                foreach (Order o in _orderBLL.GetOrders())
                {
                    if (o.PrijsopgaveId == orderID)
                    {
                        Order = o;
                        break;
                    }
                }
                BTW = (int)(Order.BTW * 100);
                OrderRules = _orderBLL.GetOrderRules((int)orderID);
            }

            Levertijd = _orderBLL.GetLevertijd();
            ReferentieText = _orderBLL.GetReferentieText();
            return Page();
        }

        public IActionResult OnPost()
        {
            CurrentClientId = (int)HttpContext.Session.GetInt32("clientID");
            Order.KlantId = CurrentClientId;
            Order.BTW = BTW / 100.0;
            _orderBLL.SaveOrder(Order);

            foreach (var rule in OrderRules)
            {
                _orderBLL.SaveOrderRule(rule);
            }

            foreach (var rule in NewOrderRules)
            {
                if (!string.IsNullOrWhiteSpace(rule.Artikelnummer))
                {
                    rule.PrijsopgaveId = Order.PrijsopgaveId;
                    _orderBLL.SaveOrderRule(rule);
                }
            }

            return RedirectToPage("/CustomerPage");
        }

    }
}
