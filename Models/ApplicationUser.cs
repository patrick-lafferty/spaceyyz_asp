using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(70)]
		[Required]
		public string FirstName {get; set;}

		[MaxLength(70)]
		[Required]
		public string LastName {get; set;}
	}
}
