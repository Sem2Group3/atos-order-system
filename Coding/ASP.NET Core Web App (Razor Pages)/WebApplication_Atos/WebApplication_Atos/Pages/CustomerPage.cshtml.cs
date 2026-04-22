using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MySqlConnector;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    [Authorize(Roles = "orders, finance, produce")]
    public class CustomerPageModel : PageModel
    {
        private readonly IOrderBLL _orderBLL;
        private readonly IClientBLL _clientBLL;
        private readonly IOrderPrintedFlagBLL _orderPrintedFlagBLL;
        private readonly IFlagQuotationBLL _flagQuotationBLL;
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IOrderFlagBLL _orderFlagBLL;
        private readonly MySqlDataSource _dataSource;
        [BindProperty]
        public Client currentClient { get; set; }
        public Client newClient { get; private set; }
        public int currentClientID { get; private set; } = -1;
        public List<Client> Clients { get; private set; } = new List<Client>();
        [BindProperty]
        public bool changeMode { get; set; }
        public List<Order> UserOrders { get; private set; } = new List<Order>();
        public List<PrintedFlag> CurrentClientPrintedFlags { get; set; }
        public List<FlagQuotation> CurrentClientFlagQuotes { get; set; }
        public List<OrderFlag> FlagOrders { get; set; }


        public CustomerPageModel(MySqlDataSource dataSource, IClientBLL clientBLL, IOrderBLL orderBLL, IOrderPrintedFlagBLL orderPrintedFlagBLL, IFlagQuotationBLL flagQuotationBLL, IEmployeeBLL employeeBLL, IOrderFlagBLL orderFlagBLL)
        {
            _dataSource = dataSource;
            _clientBLL = clientBLL;
            _orderBLL = orderBLL;
            _orderPrintedFlagBLL = orderPrintedFlagBLL;
            _flagQuotationBLL = flagQuotationBLL;
            _employeeBLL = employeeBLL;
            _orderFlagBLL = orderFlagBLL;
        }
        public string GetLevancierName(int levancierID)
        {
            return _orderPrintedFlagBLL.GetLevancierName(levancierID);
        }
        public int GetQuantity(int orderID)
        {
            return _orderPrintedFlagBLL.GetPrinteFlagRules(orderID).Hoeveelheid;
        }
        public string GetEmployeeName(int employeeID)
        {
            foreach (var emp in _employeeBLL.GetEmployees())
            {
                if (emp.WerknemerId == employeeID)
                {
                    return emp.Werknemer;
                }
            }
            return null;
        }
        public List<FlagQuotationRule> GetFlagQuotationRules(FlagQuotation flagQuotation)
        {
            return _flagQuotationBLL.GetFlagQuotationRules((int)flagQuotation.PrijsopgaveId);
        }
        public double GetTotal(OrderFlag orderFlag)
        {
            double total = 0;
            foreach (OrderFlagRule o in _orderFlagBLL.GetOrderFlagRules(orderFlag.OrderId))
            {
                total += (double)o.Prijs;
            }
            return Math.Round(total * (1 + (double)orderFlag.BTW),2);
        }

        public async Task<IActionResult> OnGetAsync()
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

            int? clientID = HttpContext.Session.GetInt32("clientID");



            Clients = await _clientBLL.GetClientsAsync();
            if (Clients.Count > 0)
            {
                if (clientID == null)
                {
                    clientID = Clients[0].Klantid;
                    HttpContext.Session.SetInt32("clientID", (int)clientID);
                }
                currentClientID = Convert.ToInt32(clientID);
                currentClient = _clientBLL.GetClientFromID(currentClientID);
                UserOrders = _orderBLL.GetOrders(currentClientID);
                CurrentClientPrintedFlags = _orderPrintedFlagBLL.GetPrintedFlags(currentClientID);
                CurrentClientFlagQuotes = _flagQuotationBLL.GetFlagQuotationsByKlantID(currentClientID);
                FlagOrders = _orderFlagBLL.GetOrderFlags(currentClientID);
            }
            return Page();
        }
        
        public double CalculateNetto(Order order)
        {
            double netto = 0;

            foreach (OrderRule o in _orderBLL.GetOrderRules(order.PrijsopgaveId))
            {
               netto +=(double) o.Prijs*o.Hoeveelheid;
            }
            return netto;
        }
        public string SerializeOrder(Order offerte)
        {
            return JsonSerializer.Serialize(offerte);
        }
        public string SerializePrintedFlagRule(FlagQuotationRule rule)
        {
            return JsonSerializer.Serialize(rule);
        }
        public string SerializePrintedFlag(FlagQuotation offerte)
        {
            return JsonSerializer.Serialize(offerte);
        }

        public async Task<IActionResult> OnPostAsync(string postString, int postItem, bool changeMode)
        {
            Clients = _clientBLL.GetClients();
            this.changeMode = changeMode;
            if (postString == "EnterEditMode")
            {
                this.changeMode = true;
                await OnGetAsync();
                return Page();
            }
            else if (postString == "SaveChanges")
            {
                _clientBLL.UpdateClient(currentClient);
                this.changeMode = false;
            }
            else if (postString == "SelectSearchClientID")
            {
                currentClientID = postItem;
                HttpContext.Session.SetInt32("clientID", currentClientID);
            }
            else if (postString == "SaveClientButtonClick")
            {
                this.changeMode = false;
                _clientBLL.AddClient(currentClient);
            }
            else if (postString == "NewClientButtonClick")
            {
                await OnGetAsync(); 
                currentClient = new Client();
                currentClientID = currentClient.Klantid;
                this.changeMode = true;
                ModelState.Clear();
                return Page();
            }

            return RedirectToPage();
        }
        public IActionResult OnPostEditOrder(int orderID)
        {
            HttpContext.Session.SetInt32("orderID", orderID);
            return RedirectToPage("/SubCustomerPage");
        }
        public IActionResult OnPostEditOrderPrintedFlag(int orderID)
        {
            HttpContext.Session.SetInt32("printedFlagID", orderID);
            return RedirectToPage("/OrderBedrukteVlaggenVoorDuikverenigingAdFundumUitDrunen");
        }
        public IActionResult OnPostNewOrder()
        {
            HttpContext.Session.Remove("printedFlagID");
            return RedirectToPage("/OrderBedrukteVlaggenVoorDuikverenigingAdFundumUitDrunen");
        }
        public IActionResult OnPostEditFlagQuote(int PrijsopgaveId)
        {
            HttpContext.Session.SetInt32("flagQuoteID", PrijsopgaveId);
            return RedirectToPage("/PrijpopgavebedruktevlaggenvoorDuikverenigingAdFundumuitDrunen");
        }
        public IActionResult OnPostNewFlagQuote()
        {
            HttpContext.Session.Remove("flagQuoteID");
            return RedirectToPage("/PrijpopgavebedruktevlaggenvoorDuikverenigingAdFundumuitDrunen");
        }
        public IActionResult OnPostEditPrijsogave(int PrijsopgaveId)
        {
            HttpContext.Session.SetInt32("prisjogaveID", PrijsopgaveId);
            return RedirectToPage("/SubCustomerPage");
        }
        public IActionResult OnPostNewPrijsogave()
        {
            HttpContext.Session.Remove("prisjogaveID");
            return RedirectToPage("/SubCustomerPage");
        }

        public IActionResult OnPostEditOrderFlag(int OrderFlagId)
        {
            HttpContext.Session.SetInt32("orderFlagId", OrderFlagId);
            return RedirectToPage("/OrdervlaggenmastenvoorDuikverenigingAdFundumuitDrunen");
        }
        public IActionResult OnPostNewOrderFlag()
        {
            HttpContext.Session.Remove("orderFlagId");
            return RedirectToPage("/OrdervlaggenmastenvoorDuikverenigingAdFundumuitDrunen");

        }

        public IActionResult OnPostNewOrderFlagPrijsopgave(string serializedOrder)
        {
            HttpContext.Session.Remove("orderFlagId");
            HttpContext.Session.SetString("serializedOrder", serializedOrder);
            return RedirectToPage("/OrdervlaggenmastenvoorDuikverenigingAdFundumuitDrunen");
        }
        
        public IActionResult OnPostNewPrintedFlagPrijsopgave(string serializedPrintedFlagRule, string serializedPrintedFlag, string NaamVlag)
        {
            HttpContext.Session.Remove("printedFlagID");
            HttpContext.Session.SetString("serializedPrintedFlagRule", serializedPrintedFlagRule);
            HttpContext.Session.SetString("serializedPrintedFlag", serializedPrintedFlag);
            HttpContext.Session.SetString("NaamVlag", NaamVlag);
            return RedirectToPage("/OrderBedrukteVlaggenVoorDuikverenigingAdFundumUitDrunen");
        }
    }
}

