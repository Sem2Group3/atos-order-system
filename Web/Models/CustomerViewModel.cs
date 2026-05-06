using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class InvoiceInfoViewModel
{
    [Display(Name = "Naam")]
    [Required(ErrorMessage = "Naam is verplicht")]
    [StringLength(100, ErrorMessage = "Naam mag niet langer zijn dan 100 tekens")]
    public string? Name { get; set; }

    [Display(Name = "Straat")]
    [StringLength(100, ErrorMessage = "Straat mag niet langer zijn dan 100 tekens")]
    public string? Street { get; set; }

    [Display(Name = "Huisnummer")]
    [StringLength(10, ErrorMessage = "Huisnummer mag niet langer zijn dan 10 tekens")]
    public string? HouseNumber { get; set; }

    [Display(Name = "Postcode")]
    [StringLength(10, ErrorMessage = "Postcode mag niet langer zijn dan 10 tekens")]
    public string? PostalCode { get; set; }

    [Display(Name = "Plaats")]
    [StringLength(100, ErrorMessage = "Plaats mag niet langer zijn dan 100 tekens")]
    public string? City { get; set; }

    [Display(Name = "Factuurbetrekking")]
    [StringLength(200, ErrorMessage = "Factuurbetrekking mag niet langer zijn dan 200 tekens")]
    public string? InvoiceRegarding { get; set; }

    [Display(Name = "BTW-nummer")]
    [StringLength(50, ErrorMessage = "BTW-nummer mag niet langer zijn dan 50 tekens")]
    public string? VatNumber { get; set; }

    [Display(Name = "Opmerkingen")]
    [StringLength(1000, ErrorMessage = "Opmerkingen mag niet langer zijn dan 1000 tekens")]
    public string? ContactNotes { get; set; }
}

public class CustomerViewModel
{
    public int? Id { get; set; }

    [Display(Name = "Volledige naam")]
    [Required(ErrorMessage = "Volledige naam is verplicht")]
    [StringLength(100, ErrorMessage = "Volledige naam mag niet langer zijn dan 100 tekens")]
    public string? FullName { get; set; }

    [Display(Name = "Straat")]
    [StringLength(100, ErrorMessage = "Straat mag niet langer zijn dan 100 tekens")]
    public string? Street { get; set; }

    [Display(Name = "Huisnummer")]
    [StringLength(10, ErrorMessage = "Huisnummer mag niet langer zijn dan 10 tekens")]
    public string? HouseNumber { get; set; }

    [Display(Name = "Postcode")]
    [StringLength(10, ErrorMessage = "Postcode mag niet langer zijn dan 10 tekens")]
    public string? PostalCode { get; set; }

    [Display(Name = "Plaats")]
    [StringLength(100, ErrorMessage = "Plaats mag niet langer zijn dan 100 tekens")]
    public string? City { get; set; }

    [Display(Name = "Telefoonnummer")]
    [Phone(ErrorMessage = "Geldig telefoonnummer is vereist")]
    [StringLength(20, ErrorMessage = "Telefoonnummer mag niet langer zijn dan 20 tekens")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "E-mail")]
    [EmailAddress(ErrorMessage = "Geldig e-mailadres is vereist")]
    [StringLength(100, ErrorMessage = "E-mail mag niet langer zijn dan 100 tekens")]
    public string? Email { get; set; }

    [Display(Name = "Verzending gericht naar")]
    [StringLength(200, ErrorMessage = "Verzending gericht naar mag niet langer zijn dan 200 tekens")]
    public string? MailAddressedTo { get; set; }

    [Display(Name = "Opmerkingen")]
    [StringLength(1000, ErrorMessage = "Opmerkingen mogen niet langer zijn dan 1000 tekens")]
    public string? Notes { get; set; }

    [Display(Name = "Openstaand bedrag")]
    public decimal OutstandingAmount { get; set; }

    [Display(Name = "Factuurgegevens")]
    public InvoiceInfoViewModel InvoiceInformation { get; set; } = new InvoiceInfoViewModel();
}

