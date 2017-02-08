using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpaceYYZ.Models;

namespace SpaceYYZ.ViewComponents
{
	public class NavigationViewComponent : ViewComponent
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public NavigationViewComponent(
				UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			/* if the user is logged in, check which role
			 * they have (and is currently active)
			 * and return the appropriate view
			 * */
			ViewBag.getTabClass = new Func<string, string>(GetTabClass); 

			var user = await _userManager.GetUserAsync(this.UserClaimsPrincipal);
			
			if (user == null)
			{
				return View("NotLoggedIn");
			}
			else if (!string.IsNullOrEmpty(user.CurrentRole))
			{
				return View(user.CurrentRole);
			}
			else
			{
				return View();
			}

		}

		public string GetTabClass(string tabName)
		{
			var name = this.ViewContext.ActionDescriptor.DisplayName;		

			var start = 21;
			var actionName = name.Substring(start, name.Length - 15 - start);

			System.Console.WriteLine("comparing _" + tabName + "_ " + "with _" + actionName + "_");

			if (tabName == actionName)
			{
				return "active-tab";
			}
			else
			{
				return "inactive-tab";
			}
		}
	}
}
