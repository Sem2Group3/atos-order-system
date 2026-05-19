using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Data;

public class ApplicationUser : IdentityUser
{
	[MaxLength(50)]
	public required string FirstName { get; set; }
	
	[MaxLength(50)]
	public required string LastName { get; set; }
}

