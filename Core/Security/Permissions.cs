namespace Core.Security;

public static class Permissions
{
    public const string InvoiceCreate = "invoice.create";
    public const string InvoiceUpdate = "invoice.update";
    public const string InvoiceDelete = "invoice.delete";
    public const string InvoiceView   = "invoice.view";

    public static readonly List<string> All = 
    [
        InvoiceCreate,
        InvoiceUpdate,
        InvoiceDelete,
        InvoiceView
    ];
}