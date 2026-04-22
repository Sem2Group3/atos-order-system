using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Core.Security;

namespace Web.Extensions;

public static class IdentitySeedExtensions
{
    public static async Task SeedRolesAndPermissionsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var (roleName, permissions) in RolePermissions.PermissionsByRole)
        {
            var role = await EnsureRoleAsync(roleManager, roleName);
            await SyncRolePermissionsAsync(roleManager, role, permissions);
        }
    }

    private static async Task<IdentityRole> EnsureRoleAsync(
        RoleManager<IdentityRole> roleManager,
        string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role is not null)
            return role;

        var createRoleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (createRoleResult.Succeeded)
            return await roleManager.FindByNameAsync(roleName)
                   ?? throw new InvalidOperationException(
                       $"Role '{roleName}' was created but could not be loaded.");
        var errorText = string.Join(", ", createRoleResult.Errors.Select(e => e.Description));
        throw new InvalidOperationException($"Failed to create role '{roleName}': {errorText}");
    }

    private static async Task SyncRolePermissionsAsync(
        RoleManager<IdentityRole> roleManager,
        IdentityRole role,
        IEnumerable<string> targetPermissions)
    {
        var existingClaims = await roleManager.GetClaimsAsync(role);

        var existingPermissions = existingClaims
            .Where(c => c.Type == PermissionConstants.ClaimType)
            .Select(c => c.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var desiredPermissions = targetPermissions
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var permission in desiredPermissions.Except(existingPermissions))
        {
            var result = await roleManager.AddClaimAsync(
                role,
                new Claim(PermissionConstants.ClaimType, permission));

            if (result.Succeeded) continue;
            var errorText = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(
                $"Failed to add permission '{permission}' to role '{role.Name}': {errorText}");
        }

        foreach (var staleClaim in existingClaims.Where(c =>
                     c.Type == PermissionConstants.ClaimType &&
                     !desiredPermissions.Contains(c.Value)))
        {
            var result = await roleManager.RemoveClaimAsync(role, staleClaim);

            if (result.Succeeded) continue;
            var errorText = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(
                $"Failed to remove permission '{staleClaim.Value}' from role '{role.Name}': {errorText}");
        }
    }
}