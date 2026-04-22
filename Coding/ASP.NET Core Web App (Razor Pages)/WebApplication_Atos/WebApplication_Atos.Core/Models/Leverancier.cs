using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Atos.Core.Models
{
    public class Leverancier
    {
        public int LeverancierId { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Woonplaats { get; set; }
    }
}
