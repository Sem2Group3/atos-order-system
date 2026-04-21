using Microsoft.AspNetCore.Identity;

namespace Core.Auth;

public interface IAuthService
{
    Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
    Task<IdentityResult> RegisterAsync(string email, string password);
    Task LogoutAsync();
}