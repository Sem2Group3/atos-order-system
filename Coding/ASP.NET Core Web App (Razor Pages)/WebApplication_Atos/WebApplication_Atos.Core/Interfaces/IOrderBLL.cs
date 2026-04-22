using System;
using WebApplication_Atos.Core.Models;
using WebApplication_Atos.Models;

namespace WebApplication_Atos.Core.Interfaces;

public interface IOrderBLL
{
    void SaveOrder(Order order);
    void SaveOrderRule(OrderRule rule);
    List<Order> GetOrders();
    List<Order> GetOrders(int clientId);
    List<OrderRule> GetOrderRules(int orderId);
    List<Order> GetLevertijd();
    List<Order> GetReferentieText();
}
