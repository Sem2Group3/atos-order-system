using Microsoft.AspNetCore.Identity;
using Core.Security;

namespace Core.Auth;

public class AuthService(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    : IAuthService
{
    public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
    {
        return await signInManager.PasswordSignInAsync(
            email, password, rememberMe, false);
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
        };
        
        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return createResult;

        var addToRoleResult = await userManager.AddToRoleAsync(user, Roles.Viewer);
        return addToRoleResult.Succeeded ? createResult : addToRoleResult;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}