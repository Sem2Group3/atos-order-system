using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class InvoiceDetailsViewModel
{
    [Display(Name = "Naam")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Postcode")]
    public string PostalCode { get; set; } = string.Empty;

    [Display(Name = "Woonplaats")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Factuur t.a.v.")]
    public string InvoiceAttentionOf { get; set; } = string.Empty;

    [Display(Name = "Digitale factuur")]
    public string DigitalInvoice { get; set; } = string.Empty;

    [Display(Name = "BTW-nummer")]
    public string VatNumber { get; set; } = string.Empty;

    [Display(Name = "Contact notities")]
    public string ContactNotes { get; set; } = string.Empty;
}