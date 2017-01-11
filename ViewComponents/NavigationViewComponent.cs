using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
			/*if (this.User.IsInRole("Administration"))
			{
				return View("Administration");
			}
			else
			{
				return View();
			}*/

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
	}
}
