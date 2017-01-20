using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class RoleViewModel
	{
		[Required]
		public string Name {get; set;}

		[Required]
		public int UsersAssigned {get; set;}

		[Required]
		public string Description {get; set;}

	}
}
