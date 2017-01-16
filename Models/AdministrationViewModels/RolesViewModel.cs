using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpaceYYZ.Models.AdministrationViewModels
{
	public class RolesViewModel
	{
		public string Username {get; set;}
		public string Id {get; set;}
		
		public string NewRole {get; set;}
		public List<SelectListItem> AvailableRoles {get; set;}
	}
}
