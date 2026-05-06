namespace Core.Security;

public static class RolePermissions
{
    public static readonly Dictionary<string, List<string>> PermissionsByRole = new()
    {
        [Roles.Admin] = Permissions.All.ToList(),
        
        [Roles.Accountant] =
        [
            Permissions.Invoice.Create,
            Permissions.Invoice.Update,
            Permissions.Invoice.View,
            Permissions.Customer.Create,
            Permissions.Customer.Update,
            Permissions.Customer.View,
            Permissions.Dashboard.View
        ],
        [Roles.Viewer] =
        [
            Permissions.Invoice.View,
            Permissions.Customer.View,
            Permissions.Dashboard.View
        ]
    };
}