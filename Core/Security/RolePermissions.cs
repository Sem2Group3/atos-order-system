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
            Permissions.Dashboard.View
        ],
        [Roles.Viewer] =
        [
            Permissions.Invoice.View,
            Permissions.Dashboard.View
        ]
    };
}