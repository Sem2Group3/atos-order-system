using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;
using System.Security.Claims;

namespace WebApplication_Atos.BLL.Services
{
    public class Authenticator
    {
        private IEmployeeBLL _employeeService;

        public Authenticator(IEmployeeBLL employeeService)
        {
            _employeeService = employeeService;
        }

        public bool AuthenticateUser(string username, string password)
        {
            // error handling parameters

            // check if username exists
            Employee? employee = _employeeService.GetEmployeeByUsername(username);
            if (employee == null) return false;

            // check if password matches PHC
            string? existingPHC = _employeeService.GetPHCByUsername(username);
            if (existingPHC == null) return false; // ?? error logic

            return Hasher.ComparePasswordToPHC(password, existingPHC);
        }
    }
}
