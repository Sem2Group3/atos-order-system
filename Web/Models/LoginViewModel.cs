using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; init; } = string.Empty;

    [Display(Name = "Onthoud mij")]
    public bool RememberMe { get; init; }
}