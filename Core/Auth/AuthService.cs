using Microsoft.AspNetCore.Identity;
using Core.Security;
using Data;

namespace Core.Auth;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : IAuthService
{
    public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
    {
        return await signInManager.PasswordSignInAsync(
            email, password, rememberMe, false);
    }

    public async Task<IdentityResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        return await CreateUserAsync(firstName, lastName, email, password, Roles.Viewer);
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
    
    public async Task<IdentityResult> CreateUserAsync(string firstName, string lastName, string email, string password, string roleName)
    {
        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = email,
            Email = email,
        };
        
        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return createResult;

        var addToRoleResult = await userManager.AddToRoleAsync(user, roleName);
        return addToRoleResult.Succeeded ? createResult : addToRoleResult;
    }

    public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }
}