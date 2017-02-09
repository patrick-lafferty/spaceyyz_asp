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
			ViewBag.getControllerTabClass = new Func<string, string>(GetControllerTabClass); 
			ViewBag.getActionTabClass = new Func<string, string>(GetActionTabClass); 

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

		public string GetCurrentActionDisplayName()
		{
			if (!ViewData.ContainsKey("CurrentActionDisplayName"))
			{
				var name = this.ViewContext.ActionDescriptor.DisplayName;		

				var start = 21; //length of SpaceYYZ.Controllers.
				var actionName = name.Substring(start, name.Length - 15 - start);  //length of (spaceyyz_asp)

				ViewData["CurrentActionDisplayName"] = actionName;
				return actionName;
			}

			return ViewData["CurrentActionDisplayName"] as string;
		}

		public string GetCurrentAction()
		{
			if (!ViewData.ContainsKey("CurrentAction"))
			{
				var displayName = GetCurrentActionDisplayName();
				displayName = displayName.Substring(displayName.IndexOf(".") + 1);
				ViewData["CurrentAction"] = displayName;
				return displayName;
			}

			return ViewData["CurrentAction"] as string;
		}

		public string GetCurrentController()
		{
			if (!ViewData.ContainsKey("CurrentController"))
			{
				var displayName = GetCurrentActionDisplayName();
				ViewData["CurrentController"] = displayName.Substring(0, displayName.IndexOf("Controller"));

			}

			return ViewData["CurrentController"] as string;
		}

		public string GetControllerTabClass(string controllerName)
		{
			var currentController = GetCurrentController();

			return currentController == controllerName ? "active-tab" : "inactive-tab";
		}

		public string GetActionTabClass(string actionName)
		{
			var currentAction = GetCurrentAction();

			return currentAction == actionName ? "active-tab" : "inactive-tab";
		}
	}
}
