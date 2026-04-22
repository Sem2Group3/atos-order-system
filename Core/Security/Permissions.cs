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

    public static class Dashboard
    {
        public const string Read = "dashboard.read";
    }

    public static readonly IReadOnlyList<string> All =
    [
        Invoice.Create,
        Invoice.Update,
        Invoice.Delete,
        Invoice.View,
        
        Dashboard.Read
    ];
}