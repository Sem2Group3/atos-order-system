using System;
using WebApplication_Atos.Interfaces;
using WebApplication_Atos.Models;

namespace WebApplication_Atos.BLL.BLL;

public class ItemBLL : IItemBLL
{
    private readonly IItemDBManager _itemDBManager;

    public ItemBLL(IItemDBManager itemDBManager)
    {
        _itemDBManager = itemDBManager;
    }

    public List<Item> GetItems()
    {
        return _itemDBManager.GetItems();
    }
}