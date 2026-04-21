using Microsoft.AspNetCore.Identity;

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
        
        return await userManager.CreateAsync(user, password);;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}