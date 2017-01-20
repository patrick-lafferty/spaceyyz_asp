using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models
{
	public class ApplicationRole : IdentityRole
	{
		[MaxLength(200)]
		public string Description {get; set;}
	}
}
