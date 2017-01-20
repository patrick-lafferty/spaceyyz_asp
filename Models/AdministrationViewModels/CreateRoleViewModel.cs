using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string Name {get; set;}

		public string Description {get; set;}
	}
}
