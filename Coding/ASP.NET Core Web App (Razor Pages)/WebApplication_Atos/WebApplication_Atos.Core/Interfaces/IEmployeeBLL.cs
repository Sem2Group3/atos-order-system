using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
    public interface IEmployeeBLL
    {
        bool SetEmployeePHC(string username, string phc);
        List<Employee> GetEmployees();
        string? GetEmployeePassword(string userName); // oud, verwijder
        Employee? GetEmployeeByUsername(string username);
        string? GetPHCByUsername(string username);
    }
}
