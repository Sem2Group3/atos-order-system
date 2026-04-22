using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Atos.Core.Models
{
    public class FlagQuotationRule
    {
       public int PrijsopgaveRegelId { get; set; }
        public int? PrijsopgaveId { get; set; }
        public  string?  Formaat {  get; set; }
        public string?  Type { get; set; }
        public string? Kleuren { get; set; }
        public int Hoeveelheid { get; set; }
        public decimal Prijs {  get; set; }
        public decimal Inkoopprijs { get; set; }
    }
}
