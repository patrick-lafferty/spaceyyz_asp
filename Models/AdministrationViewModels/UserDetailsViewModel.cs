using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class UserDetailsViewModel
	{
		public string Username {get; set;}

		public string Id {get; set;}

		[Required]
		[Display(Name = "First Name")]
		public string FirstName {get; set;}

		public string LastName {get; set;}

		public IList<string> Roles {get; set;} 
	}
}
