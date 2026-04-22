using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication_Atos.Core.Models
{
    public class PrintedFlagDetails
    {
        public int BevestigingId { get; set; }
        public string? Bevestiging { get; set; }
        public int MateriaalId { get; set; }
        public string? Materiaal { get; set; }
        public int BetalingId { get; set; }
        public string? Betaling {  get; set; }
        public Leverancier Leverancier { get; set; }
        public string? Naam {  get; set; }
        public string? Adres { get; set; }
        public string? Woonplaats { get; set; }
        public string? Postcode { get; set; }
        public string? Telefoon { get; set; }
        public string? Fax { get; set; }
        public string? EmailAdres { get; set; }
        public string? Contactpersoon { get; set; }
        public string? Notities { get; set; }
        public int VerpakkingId { get; set; }
        public string? Verpakking { get; set; }
        public int BeeldmateriaalId { get; set; }
        public string? Beeldmateriaal { get; set; }
        public int AfwerkingId { get; set; }
        public string? Afwerking { get; set; }
        public int ReferentieId { get; set; }
        public string? ReferentieText { get; set; }
        public int LeveringId { get; set; }
        public string? Levering { get; set; }
        public int StickerId { get; set; }
        public string? Sticker { get; set; }
    } 
}
