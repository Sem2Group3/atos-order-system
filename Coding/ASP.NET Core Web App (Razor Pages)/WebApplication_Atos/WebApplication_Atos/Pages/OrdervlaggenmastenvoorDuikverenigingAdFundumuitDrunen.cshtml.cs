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
    public class OrdervlaggenmastenvoorDuikverenigingAdFundumuitDrunenModel : PageModel
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IItemBLL _itemBLL;
        private readonly IOrderFlagBLL _orderFlagBLL;
        private readonly IClientBLL _clientBLL;
        private readonly IOrderBLL _orderBLL;
        private readonly MySqlDataSource _dataSource;
        public int CurrentClientId { get; set; }
        public Client CurrentClient { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        [BindProperty]
        public OrderFlag OrderFlag { get; set; } = new OrderFlag();
        [BindProperty]
        public List<OrderFlagRule> OrderFlagRules { get; set; } = new List<OrderFlagRule>();
        [BindProperty]
        public List<OrderFlagRule> NewOrderFlagRules { get; set; } = new List<OrderFlagRule>();
        public List<OrderFlag> Ref{ get; set; }
        public List <OrderFlag> Betaling {  get; set; } 
        public List<OrderFlag> Bevestiging { get; set; }
        [BindProperty]
        public int BTW { get; set; } = 21;

        public OrdervlaggenmastenvoorDuikverenigingAdFundumuitDrunenModel (MySqlDataSource dataSource ,IOrderBLL orderBLL ,IEmployeeBLL employeeBLL, IItemBLL itemBLL, IOrderFlagBLL orderFlagBLL, IClientBLL clientBLL)
        {
            _employeeBLL = employeeBLL;
            _itemBLL = itemBLL;
            _orderFlagBLL = orderFlagBLL;
            Items = _itemBLL.GetItems();
            Employees = _employeeBLL.GetEmployees();
            _clientBLL = clientBLL;
            _orderBLL = orderBLL;
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
            int? orderID = HttpContext.Session.GetInt32("orderFlagId");
            string? serializedOrder = HttpContext.Session.GetString("serializedOrder");
            if (orderID != null)
            {
                foreach (OrderFlag o in _orderFlagBLL.GetOrderFlags())
                {
                    if (o.OrderId == orderID)
                    {
                        OrderFlag = o;
                        break;
                    }
                }
                BTW = (int)(OrderFlag.BTW * 100);
                OrderFlagRules = _orderFlagBLL.GetOrderFlagRules((int)orderID);
            }
            else if(serializedOrder != null)
            {
                Order order = JsonSerializer.Deserialize<Order>(serializedOrder);
                OrderFlag.Bijzonderheden = order.Bijzonderheden;
                OrderFlag.WerknemerId = order.WerknemerId;
                OrderFlag.Notities = order.Notities;
                OrderFlag.PrijsopgaveId = order.PrijsopgaveId;
                BTW = (int)(order.BTW * 100);

                List<OrderRule> orderRules = _orderBLL.GetOrderRules(order.PrijsopgaveId);
                foreach (OrderRule rule in orderRules)
                {
                    OrderFlagRule newOrderFlagRule= new OrderFlagRule() { Hoeveelheid=(short)rule.Hoeveelheid, Prijs=rule.Prijs, Artikelnummer = rule.Artikelnummer, Omschrijving=rule.Omschrijving,Inkoopprijs=rule.InkoopPrijs,Sorteervolgorde=rule.Sorteervolgorde };
                    OrderFlagRules.Add(newOrderFlagRule);
                }
            }

            Bevestiging = _orderFlagBLL.GetOrderBevestigingen();
            Betaling = _orderFlagBLL.GetOrderBetaling();
            Ref = _orderFlagBLL.GetReferentieText();
            return Page();
        }

        public IActionResult OnPost()
        {
            CurrentClientId = (int)HttpContext.Session.GetInt32("clientID");
            OrderFlag.KlantId = CurrentClientId;
            OrderFlag.BTW = BTW / 100.0;
            _orderFlagBLL.SaveOrderFlag(OrderFlag);

            foreach (var rule in OrderFlagRules)
            {
                _orderFlagBLL.SaveOrderFlagRule(rule);
            }

            foreach (var rule in NewOrderFlagRules)
            {
                if (!string.IsNullOrWhiteSpace(rule.Artikelnummer))
                {
                    rule.OrderId = OrderFlag.OrderId;
                    _orderFlagBLL.SaveOrderFlagRule(rule);
                }
            }
            HttpContext.Session.Remove("serializedOrder");
            return RedirectToPage("/CustomerPage");
        }
    }
}
