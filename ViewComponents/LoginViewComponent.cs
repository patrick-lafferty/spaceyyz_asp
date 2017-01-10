using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SpaceYYZ.Models;

namespace SpaceYYZ.ViewComponents
{
	public class LoginViewComponent : ViewComponent
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger _logger;

		public LoginViewComponent(
				UserManager<ApplicationUser> userManager,
				SignInManager<ApplicationUser> signInManager,
				ILoggerFactory loggerFactory)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = loggerFactory.CreateLogger<LoginViewComponent>();
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (_signInManager.IsSignedIn(this.UserClaimsPrincipal))
			{
				return View("SignedIn", await _userManager.GetUserAsync(this.UserClaimsPrincipal));
			}

			return View();
		}
	}
}
