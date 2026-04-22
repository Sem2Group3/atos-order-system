using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces
{
     public interface IEmployeeDBManager
    {
        bool SetEmployeePHC(string username, string phc);
        List<Employee> GetEmployees();
        string? GetEmployeePassword(string userName);
        Employee? GetEmployeeByUsername(string username);
        string? GetPHCByUsername(string username);
    }
}
