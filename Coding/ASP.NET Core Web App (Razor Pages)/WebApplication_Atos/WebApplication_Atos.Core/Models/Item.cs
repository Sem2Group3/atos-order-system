namespace WebApplication_Atos.Models
{
    public class Item
    {
        public int Artikelgroep { get; set; }
        public int Artikelsubgroep { get; set; }
        public string? Artikelnummer { get; set; }
        public string? Omschrijving { get; set; }
        public string? OmschrijvingLang { get; set; }
        public string? OmschrijvingKoppeling { get; set; }
        public int FoormaatId { get; set; }
        public Nullable<int> MasId { get; set; }
        public decimal Prijs { get; set; }
        public decimal? ActiePrijs { get; set; }
        public decimal? InkoopPrijs { get; set; }
        public string? Webwinkel { get; set; }
        public string? VoorraadArtikel { get; set; }
        public string? Afbeelding { get; set; }
        public int? Aantal { get; set; }
        public DateTime? Actievan { get; set; }
        public DateTime? Actietot { get; set; }
        public string? Verzendkosten { get; set; }
        public DateTime? DatumWijziging { get; set; }
        public int? Sorteervolgorde {  get; set; }
    }
}