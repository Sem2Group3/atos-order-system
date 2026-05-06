using Web.Models;

namespace Web.Services;

public class CustomerService : ICustomerService
{
    private static int _nextId = 1;
    private static readonly Dictionary<int, CustomerViewModel> Customers = new();

    public CustomerService()
    {
        if (Customers.Count == 0) SeedFakeCustomers();
    }

    private void SeedFakeCustomers()
    {
        Customers[_nextId] = new CustomerViewModel 
        { 
            Id = _nextId++, 
            FullName = "Jan de Vries", 
            Street = "Dorpsstraat", 
            HouseNumber = "12", 
            PostalCode = "1234 AB", 
            City = "Amsterdam", 
            PhoneNumber = "+31201234567", 
            Email = "jan.devries@example.com", 
            MailAddressedTo = "Jan de Vries", 
            Notes = "Belangrijk contactmoment op maandagochtend.",
            OutstandingAmount = 124.50m,
            InvoiceInformation = new() 
            { 
                Name = "De Vries BV", 
                Street = "Dorpsstraat",
                HouseNumber = "12",
                PostalCode = "1234 AB",
                City = "Amsterdam",
                VatNumber = "NL123456789B01",
                ContactNotes = "Factureren per e-mail"
            } 
        };
        Customers[_nextId] = new CustomerViewModel 
        { 
            Id = _nextId++, 
            FullName = "Marie Jansen", 
            Street = "Marktplein", 
            HouseNumber = "5A", 
            PostalCode = "2000 CD", 
            City = "Rotterdam", 
            PhoneNumber = "+31101234567", 
            Email = "marie.jansen@example.com", 
            MailAddressedTo = "M. Jansen", 
            Notes = "Voorkeur voor digitale communicatie.",
            OutstandingAmount = 0m,
            InvoiceInformation = new() 
            { 
                Name = "Jansen Consultancy", 
                Street = "Marktplein",
                HouseNumber = "5A",
                PostalCode = "2000 CD",
                City = "Rotterdam",
                VatNumber = "NL987654321B01",
                ContactNotes = "Betaalt meestal op tijd"
            } 
        };
        Customers[_nextId] = new CustomerViewModel 
        { 
            Id = _nextId++, 
            FullName = "Pieter van der Berg", 
            Street = "Stationsplein", 
            HouseNumber = "20", 
            PostalCode = "3000 EF", 
            City = "Utrecht", 
            PhoneNumber = "+31302345678", 
            Email = "pieter.vdberg@example.com", 
            MailAddressedTo = "Pieter van der Berg", 
            Notes = "Regelmatig contact over nieuwe opdrachten.",
            OutstandingAmount = 389.99m,
            InvoiceInformation = new() 
            { 
                Name = "Van der Berg Handel", 
                Street = "Stationsplein",
                HouseNumber = "20",
                PostalCode = "3000 EF",
                City = "Utrecht",
                VatNumber = "NL555666777B01",
                ContactNotes = "Altijd eerst bellen voor factuurwijzigingen"
            } 
        };
    }

    public Task<CustomerViewModel?> GetCustomerByIdAsync(int id) 
    { 
        Customers.TryGetValue(id, out var c); 
        return Task.FromResult(c); 
    }

    public Task<List<CustomerViewModel>> GetAllCustomersAsync() 
        => Task.FromResult(Customers.Values.OrderBy(c => c.FullName).ToList());

    public Task<List<CustomerViewModel>> SearchCustomersAsync(string? s) 
    { 
        var q = Customers.Values.AsEnumerable(); 
        if (!string.IsNullOrWhiteSpace(s)) 
        { 
            var l = s.ToLower(); 
            q = q.Where(c => 
                (c.FullName?.ToLower().Contains(l) ?? false) || 
                (c.Email?.ToLower().Contains(l) ?? false)); 
        } 
        return Task.FromResult(q.OrderBy(c => c.FullName).ToList()); 
    }

    public Task<int> CreateCustomerAsync(CustomerViewModel c) 
    { 
        c.Id = _nextId++; 
        Customers[c.Id.Value] = c; 
        return Task.FromResult(c.Id.Value); 
    }

    public Task<bool> UpdateCustomerAsync(int id, CustomerViewModel c) 
    { 
        if (!Customers.TryGetValue(id, out var existing)) return Task.FromResult(false); 
        c.Id = id;
        c.OutstandingAmount = existing.OutstandingAmount;
        c.InvoiceInformation.ContactNotes = existing.InvoiceInformation.ContactNotes;
        Customers[id] = c; 
        return Task.FromResult(true); 
    }

    public Task<bool> DeleteCustomerAsync(int id) 
        => Task.FromResult(Customers.Remove(id));
}








