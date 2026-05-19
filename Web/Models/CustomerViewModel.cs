using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class CustomerViewModel
{
    [Display(Name = "Klant nummer")]
    public int CustomerId { get; set; } = 0;

    public string SearchText { get; set; } = string.Empty;

    [Display(Name = "Naam")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Openstaand bedrag")]
    public decimal? OutstandingAmount { get; set; }

    [Display(Name = "Adres")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Postcode")]
    public string PostalCode { get; set; } = string.Empty;

    [Display(Name = "Woonplaats")]
    public string City { get; set; } = string.Empty;

    [Display(Name = "Telefoonnummer")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Mobiel")]
    public string MobileNumber { get; set; } = string.Empty;

    [Display(Name = "E-mailadres")]
    public string EmailAddress { get; set; } = string.Empty;

    [Display(Name = "T.a.v.")]
    public string AttentionOf { get; set; } = string.Empty;

    [Display(Name = "Notities")]
    public string Notes { get; set; } = string.Empty;

    [Display(Name = "Geblokkeerd")]
    public bool IsBlocked { get; set; }

    public InvoiceDetailsViewModel InvoiceDetails { get; set; } = new();
}