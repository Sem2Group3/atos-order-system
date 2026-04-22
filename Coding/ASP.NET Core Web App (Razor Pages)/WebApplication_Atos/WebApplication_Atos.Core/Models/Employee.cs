using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Atos.Core.Models
{
    public class Employee
    {
        public int WerknemerId { get; set; }
        public string? Role { get; set; }
        public string? Werknemer {  get; set; }
        public string? Ondergetekende {  get; set; }
        public string? PHC { get; set; }

        public override string ToString()
        {
            string toString = "";
            toString += $"WerknemerId: {WerknemerId}\n";
            toString += $"Role: {Role ?? "null"}\n";
            toString += $"Werknemer: {Werknemer}\n";
            toString += $"Ondergetekende: {Ondergetekende}\n";
            toString += $"PHC: {PHC ?? "null"}\n";

            return toString;
        }
    }
}
