using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
    public interface IOrderPrintedFlagBLL
    {
        void SaveOrderPrintedFlag(PrintedFlag newOrder);
        void SaveOrderPrintedFlagRule(PrintedFlagRule newOrderRule);
        List<PrintedFlag> GetPrintedFlags();
        PrintedFlag GetPrintedFlag(int printedFlagID);
        List<PrintedFlag> GetPrintedFlags(int clientID);
        List<PrintedFlagRule> GetPrintedFlagRules();
        PrintedFlagRule GetPrinteFlagRules(int printedFlagID);
        List<PrintedFlagDetails> GetOrderBevestigingen();
        List<PrintedFlagDetails> GetVlagMateriaal();
        List<PrintedFlagDetails> GetOrderBetaling();
        List<Leverancier> GetLeveranciers();
        string GetLevancierName(int levancierID);
        List<PrintedFlagDetails> GetVlagVerpakking();
        List<PrintedFlagDetails> GetBeeldmaterial();
        List<PrintedFlagDetails> GetVlagAfwerking();
        List<PrintedFlagDetails> GetReferentieText();
        List<PrintedFlagDetails> GetLevering();
        List<PrintedFlagDetails> GetStickers();
        List<PrintedFlagRuleDetails> GetVlagFormaat();
        List<PrintedFlagRuleDetails> GetDrukTypes();

    }
}
