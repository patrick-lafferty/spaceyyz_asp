using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceYYZ.Models;
using SpaceYYZ.Models.AccountViewModels;

namespace SpaceYYZ.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger _logger;

		public AccountController(
				UserManager<ApplicationUser> userManager,
				SignInManager<ApplicationUser> signInManager,
				ILoggerFactory loggerFactory)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = loggerFactory.CreateLogger<AccountController>();
		}

		// GET: /Account/Login
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			return View();
		}

		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, lockoutOnFailure: false);
				if (result.Succeeded)
				{

					_logger.LogInformation(1, "User logged in.");
					return Redirect(returnUrl);
				}

				if (result.IsLockedOut)
				{
					_logger.LogWarning(2, "User account locked out.");
					return View("Lockout");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt");
					return View(model);
				}
			}

			return View(model);

		}
	}
}
