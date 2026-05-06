namespace Core.Security;

public static class Permissions
{
    public static class Invoice
    {
        public const string Create = "invoice.create";
        public const string Update = "invoice.update";
        public const string Delete = "invoice.delete";
        public const string View = "invoice.view";
    }

    public static class Customer
    {
        public const string Create = "customer.create";
        public const string Update = "customer.update";
        public const string Delete = "customer.delete";
        public const string View = "customer.view";
    }

    public static class Dashboard
    {
        public const string View = "dashboard.View";
    }

    public static readonly IReadOnlyList<string> All =
    [
        Invoice.Create,
        Invoice.Update,
        Invoice.Delete,
        Invoice.View,
        
        Customer.Create,
        Customer.Update,
        Customer.Delete,
        Customer.View,
        
        Dashboard.View
    ];
}