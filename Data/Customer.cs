namespace Data;

public class Customer
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }

    public required string PhoneNumber { get; set; }
    public required string MobilePhoneNumber { get; set; }
    public required string Email { get; set; }

    public bool Blocked { get; set; }

    public required string Notes { get; set; }

    // Invoice info
    public required string InvoiceName { get; set; }
    public required string InvoiceAddress { get; set; }
    public required string InvoicePostalCode { get; set; }
    public required string InvoiceCity { get; set; }

    public required string VatNumber { get; set; }
    public required string InvoiceRegarding { get; set; }
    public required string ContactNotes { get; set; }
}