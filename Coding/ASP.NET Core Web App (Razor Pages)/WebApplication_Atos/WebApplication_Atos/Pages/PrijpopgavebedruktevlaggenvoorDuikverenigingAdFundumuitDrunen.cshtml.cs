using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using WebApplication_Atos.BLL.BLL;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class PrijpopgavebedruktevlaggenvoorDuikverenigingAdFundumuitDrunenModel : PageModel
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IFlagQuotationBLL _flagQuotationBLL;
        private readonly IClientBLL _clientBLL;
        private readonly MySqlDataSource _dataSource;


        public int CurrentClientId { get; set; }
        public Client CurrentClient { get; set; }

        public List<FlagQuotation> VlagAfwerking { get; set; }
        public List<FlagQuotation> VlagMateriaal { get; set; }
        public List<FlagQuotation> VlagVerpakking {  get; set; }
        public List<FlagQuotation> Levertijd {  get; set; }
        public List<FlagQuotation> ReferentieText { get; set; }
        public List<Employee> Employees { get; private set; } = new List<Employee>();
        [BindProperty]
        public List<FlagQuotationRule> FlagRules { get; set; } = new();
        [BindProperty]
        public FlagQuotation FlagQuote { get; set; } = new();
        [BindProperty]
        public List<FlagQuotationRule> NewFlagRules { get; set; } = new();

        [BindProperty]
        public int BTW { get; set; } = 21;

        public PrijpopgavebedruktevlaggenvoorDuikverenigingAdFundumuitDrunenModel (MySqlDataSource dataSource ,IEmployeeBLL employeeBLL, IFlagQuotationBLL flagQuotationBLL, IClientBLL clientBLL)
        {
            _clientBLL = clientBLL;
            _employeeBLL = employeeBLL;
            _flagQuotationBLL = flagQuotationBLL;
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
            int? flagQuoteId = HttpContext.Session.GetInt32("flagQuoteID");
            if(flagQuoteId != null)
            {
                FlagQuote = _flagQuotationBLL.GetFlagQuotationByID((int)flagQuoteId);
                FlagRules = _flagQuotationBLL.GetFlagQuotationRules((int)flagQuoteId);
                BTW = (int)(FlagQuote.BTW*100);
            }
            VlagAfwerking = _flagQuotationBLL.GetVlagAfwerking();
            VlagMateriaal = _flagQuotationBLL.GetVlagMateriaal();
            VlagVerpakking = _flagQuotationBLL.GetVlagVerpakking();
            Levertijd = _flagQuotationBLL.GetLevertijd();
            ReferentieText = _flagQuotationBLL.GetReferentieText();
            Employees = _employeeBLL.GetEmployees();
            return Page();
        }
        public IActionResult OnPost()
        {
            CurrentClientId = (int)HttpContext.Session.GetInt32("clientID");
            FlagQuote.KlantId = CurrentClientId;
            FlagQuote.BTW = BTW / 100.0;
            _flagQuotationBLL.SaveFlagQuotation(FlagQuote);
            foreach (var rule in FlagRules)
            {
                _flagQuotationBLL.SaveFlagQuotationRule(rule);
            }

            foreach (var rule in NewFlagRules)
            {
                if (!string.IsNullOrWhiteSpace(rule.Type))
                {
                    rule.PrijsopgaveId = FlagQuote.PrijsopgaveId;
                    _flagQuotationBLL.SaveFlagQuotationRule(rule);
                }
            }

            return RedirectToPage("/CustomerPage");
        }

    }
}
