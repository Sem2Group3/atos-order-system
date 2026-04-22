using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
     public interface IOrderFlagBLL
    {
        void SaveOrderFlag(OrderFlag order);
        void SaveOrderFlagRule(OrderFlagRule regel);
        OrderFlag GetOrderFlagByID(int orderid);
        List<OrderFlag> GetOrderFlags();
        List<OrderFlag> GetOrderFlags(int clientId);
        List<OrderFlagRule> GetOrderFlagRules(int orderId);
        List<OrderFlag> GetOrderBetaling();
        List<OrderFlag> GetOrderBevestigingen();
        List<OrderFlag> GetReferentieText();
    }
}
