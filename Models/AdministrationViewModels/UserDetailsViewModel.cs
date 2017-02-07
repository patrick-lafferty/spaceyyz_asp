using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class UserDetailsViewModel
	{
		[Required]
		public string Username {get; set;}

		[Required]
		public string Id {get; set;}

		[Required]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
		[Display(Name = "First Name")]
		public string FirstName {get; set;}

		[Required]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
		[Display(Name = "Last Name")]
		public string LastName {get; set;}

		public IList<string> Roles {get; set;} 
		public List<SelectListItem> AvailableRoles {get; set;}
	}
}
