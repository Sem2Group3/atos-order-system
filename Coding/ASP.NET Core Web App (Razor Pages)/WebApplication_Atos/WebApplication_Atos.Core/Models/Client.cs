using System;

namespace WebApplication_Atos.Core.Models;

public class Client
{
    public int Klantid { get;  set; } = 0;
    public string? Naam { get;  set; }
    public string? Adres { get;  set; }
    public string? Woonplaats { get;  set; }
    public string? Postcode { get;  set; }
    public string? Telefoon { get;  set; }
    public string? Fax { get;  set; }
    public string? Email { get;  set; }
    public string? Contactpersoon { get;  set; }
    public string? BTWnummer { get;  set; }
    public string? Notities { get;  set; }
    public int? Debiteurnummer { get;  set; }
    public string? FactuurNaam { get;  set; }
    public string? FactuurAdres { get;  set; }
    public string? FactuurWoonplaats { get;  set; }
    public string? FactuurPostcode { get;  set; }
    public string? FactuurContactpersoon { get;  set; }
    public string? NotitiesContactpersoon { get;  set; }
    public string? GevondenIn { get;  set; }
    public string? FactuurEmail { get;  set; }
    public string? Geblokkeerd { get;  set; }

}
