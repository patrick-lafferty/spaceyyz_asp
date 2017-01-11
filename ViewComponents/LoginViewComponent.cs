using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceYYZ.Models;
using SpaceYYZ.Models.AccountViewModels;

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
			    var user = await _userManager.GetUserAsync(this.UserClaimsPrincipal);
				var roles = await _userManager.GetRolesAsync(user);
				
				var model = new SignedInViewModel(user.UserName, roles) { CurrentRole = user.CurrentRole};
				
				return View("SignedIn", model);
			}

			return View();
		}
	}
}
