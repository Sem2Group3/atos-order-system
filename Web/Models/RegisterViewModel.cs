using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Voornaam")]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    [Display(Name = "Achternaam")]
    public string LastName { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; init; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord bevestigen")]
    [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
    public string ConfirmPassword { get; init; } = string.Empty;
}