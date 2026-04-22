using System.Collections.Generic;
using WebApplication_Atos.Models;

namespace WebApplication_Atos.Interfaces
{
    public interface IItemDBManager
    {
        List<Item> GetItems();
    }
}