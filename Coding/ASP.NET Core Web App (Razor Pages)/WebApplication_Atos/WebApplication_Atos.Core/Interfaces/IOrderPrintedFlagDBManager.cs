using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
    public interface IOrderPrintedFlagDBManager
    {

        void SaveOrderPrintedFlag(PrintedFlag newOrder);
        void SaveOrderPrintedFlagRule(PrintedFlagRule newOrderRule);
        List<PrintedFlag> GetPrintedFlags();
        List<PrintedFlag> GetPrintedFlags(int clientID);
        List<PrintedFlagRule> GetPrintedFlagRules();
        List<PrintedFlagDetails> GetOrderBevestigingen();
        List<PrintedFlagDetails> GetVlagMateriaal();
        List<PrintedFlagDetails> GetOrderBetaling();
        List<Leverancier> GetLeveranciers();
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
