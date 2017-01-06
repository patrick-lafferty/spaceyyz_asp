using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceYYZ.Models.AccountViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string Username {get; set;}

		[Required]
		[EmailAddress]
		public string Email {get; set;}

		[Required]
		[DataType(DataType.Password)]
		public string Password {get; set;}

		[DataType(DataType.Password)]
		[Display(Name = "Re-enter password")]
		[Compare("Password", ErrorMessage = "The two password fields must match")]
		public string PasswordConfirmation {get; set;}
	}
}
