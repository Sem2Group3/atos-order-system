using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.BLL.BLL
{
    public class OrderPrintedFlagBLL : IOrderPrintedFlagBLL
    {
        private readonly IOrderPrintedFlagDBManager _orderFlagDBManager;
        public OrderPrintedFlagBLL(IOrderPrintedFlagDBManager orderFlagDBManager)
        {
            _orderFlagDBManager = orderFlagDBManager;
        }

        public void SaveOrderPrintedFlag(PrintedFlag newOrder)
        {
            _orderFlagDBManager.SaveOrderPrintedFlag(newOrder);
        }

        public void SaveOrderPrintedFlagRule(PrintedFlagRule newOrderRule)
        {
            _orderFlagDBManager.SaveOrderPrintedFlagRule(newOrderRule);
        }

        public List<PrintedFlag> GetPrintedFlags()
        {
            return _orderFlagDBManager.GetPrintedFlags();
        }
        public PrintedFlag GetPrintedFlag(int printedFlagID)
        {
            foreach (var printedFlag in GetPrintedFlags())
            {
                if(printedFlag.OrderId == printedFlagID)
                {
                    return printedFlag;
                }
            }
            return null;
        }
        public List<PrintedFlag> GetPrintedFlags(int clientID)
        {
            return _orderFlagDBManager.GetPrintedFlags(clientID);
        }

        public List<PrintedFlagRule> GetPrintedFlagRules()
        {
            return _orderFlagDBManager.GetPrintedFlagRules();
        }
        public PrintedFlagRule GetPrinteFlagRules(int printedFlagID)
        {
            foreach (var printedFlagRules in GetPrintedFlagRules())
            {
                if (printedFlagRules.OrderId == printedFlagID)
                {
                    return printedFlagRules;
                }
            }
            return null;
        }
        public List<PrintedFlagDetails> GetOrderBevestigingen()
        {
            return _orderFlagDBManager.GetOrderBevestigingen();
        }
        public List<PrintedFlagDetails> GetVlagMateriaal()
        {
            return _orderFlagDBManager.GetVlagMateriaal();
        }
        public List<PrintedFlagDetails> GetOrderBetaling()
        {
            return _orderFlagDBManager.GetOrderBetaling();
        }
        public List<Leverancier> GetLeveranciers()
        {
            return _orderFlagDBManager.GetLeveranciers();
        }
        public string GetLevancierName(int levancierID)
        {
            foreach (var l in GetLeveranciers())
            {
                if(l.LeverancierId == levancierID)
                {
                    return l.Naam;
                }
            }
            return null;
        }
        public List<PrintedFlagDetails> GetVlagVerpakking()
        {
            return _orderFlagDBManager.GetVlagVerpakking();
        }
        public List<PrintedFlagDetails> GetBeeldmaterial()
        {
            return _orderFlagDBManager.GetBeeldmaterial();
        }
        public List<PrintedFlagDetails> GetVlagAfwerking()
        {
            return _orderFlagDBManager.GetVlagAfwerking();
        }
        public List<PrintedFlagDetails> GetReferentieText()
        {
            return _orderFlagDBManager.GetReferentieText();
        }
        public List<PrintedFlagDetails> GetLevering()
        {
            return _orderFlagDBManager.GetLevering();
        }
        public List<PrintedFlagDetails> GetStickers()
        {
            return _orderFlagDBManager.GetStickers();
        }
        public List<PrintedFlagRuleDetails> GetVlagFormaat()
        {
            return _orderFlagDBManager.GetVlagFormaat();
        }
        public List<PrintedFlagRuleDetails> GetDrukTypes()
        {
            return _orderFlagDBManager.GetDrukTypes();
        }
    }
}
