using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Pages
{
    public class FlagPrintPreviewModel : PageModel
    {
        private readonly IFlagQuotationBLL _flagQuoteBLL;
        private readonly IClientBLL _clientBLL;

        public FlagPrintPreviewModel(IFlagQuotationBLL flagQuoteBLL, IClientBLL clientBLL)
        {
            _flagQuoteBLL = flagQuoteBLL;
            _clientBLL = clientBLL;
        }

        public FlagQuotation FlagQuote { get; set; }
        public List<FlagQuotationRule> FlagRules { get; set; } = new();
        public Client CurrentClient { get; set; }

        public decimal BTWPercentage => 21m;
        public decimal NettoTotaal => FlagRules.Sum(r => r.Prijs * r.Hoeveelheid);
        public decimal BTWBedrag => NettoTotaal * (BTWPercentage / 100);
        public decimal Totaal => NettoTotaal + BTWBedrag;

        public IActionResult OnGet(int id)
        {
            FlagQuote = _flagQuoteBLL.GetFlagQuotationByID(id);
            if (FlagQuote == null) return NotFound();

            FlagRules = _flagQuoteBLL.GetFlagQuotationRules(id);
            CurrentClient = _clientBLL.GetClientFromID((int)FlagQuote.KlantId);
            return Page();
        }
    }
}
