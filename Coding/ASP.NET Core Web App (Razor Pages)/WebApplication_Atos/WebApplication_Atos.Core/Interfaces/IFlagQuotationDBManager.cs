using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
    public interface IFlagQuotationDBManager
    {
        void SaveFlagQuotation(FlagQuotation quotation);
        void SaveFlagQuotationRule(FlagQuotationRule rule);
        List<FlagQuotation> GetFlagQuotations();
        List<FlagQuotation> GetFlagQuotationsByKlantID(int klantID);
        List<FlagQuotation> GetVlagAfwerking();
        List<FlagQuotation> GetVlagMateriaal();
        List<FlagQuotation> GetVlagVerpakking();
        List<FlagQuotation> GetLevertijd();
        List<FlagQuotation> GetReferentieText();
        List<FlagQuotationRule> GetFlagQuotationRules();

        List<FlagQuotationRule> GetFlagQuotationRules(int flagQuoteID);

    }
}
