using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Voornaam")]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Achternaam")]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Wachtwoord bevestigen")]
    [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}