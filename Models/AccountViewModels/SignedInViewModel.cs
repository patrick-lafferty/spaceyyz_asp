using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpaceYYZ.Models.AccountViewModels
{
	public class SignedInViewModel
	{
		public SignedInViewModel()
		{
		}

		public SignedInViewModel(string username, IEnumerable<string> roles)
		{
			Username = username;

			AvailableRoles = roles.Select(role => new SelectListItem { 
					Value = role,
					Text = role
					}).ToList();

		}

		public string Username {get; private set;}

		public string CurrentRole {get; set;}

		public List<SelectListItem> AvailableRoles {get; private set;}
	}
}
