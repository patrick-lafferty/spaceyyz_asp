using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class DeleteUserViewModel
	{
		[Required]
		public string Username {get; set;}

		[Required]
		public string Id {get; set;}
	}
}
