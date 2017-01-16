using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceYYZ.Models;
using SpaceYYZ.Models.AdministrationViewModels;

namespace SpaceYYZ.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class AdministrationController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AdministrationController(
				UserManager<ApplicationUser> userManager,
				RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = _userManager.Users.Select(u => new UserViewModel() { 
					Id = u.Id,
					Username = u.UserName,
					FirstName = u.FirstName,
					LastName = u.LastName,
					//Roles = u.Roles.Select(r => r.RoleId).Aggregate((acc, r) => acc + r)

					});


			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}
			else
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user == null)
				{
					return NotFound();
				}
				else
				{
					var model = new UserDetailsViewModel() {
						Username = user.UserName,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Roles = await _userManager.GetRolesAsync(user)
					};

					return View(model);
				}
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}
			else
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user == null)
				{
					return NotFound();
				}
				else
				{

					var availableRoles = _roleManager.Roles.Select(r => new SelectListItem {
							Value = r.Name,
							Text = r.Name
						}).ToList();

					var model = new UserDetailsViewModel() {
						Username = user.UserName,
						Id = id,
						FirstName = user.FirstName,
						LastName = user.LastName,
						Roles = await _userManager.GetRolesAsync(user),
						AvailableRoles = availableRoles
						
					};

					return View(model);

				}

			}
		}

		[HttpGet]
		public IActionResult RemoveRole(string id, string username, string role)
		{
			if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))
			{
				var removeRoleModel = new RemoveRoleViewModel() {
					Username = username,
					Id = id,
					Role = role
				};

				return View(removeRoleModel);

			}
			else
			{
				return View();
			}
		}	

		[HttpPost]
		public async Task<IActionResult> RemoveRole(RemoveRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(model.Id);
				await _userManager.RemoveFromRoleAsync(user, model.Role);
				return RedirectToAction("Edit", new { id = user.Id });
			}
			else
			{
				return View();
			}
		}

		[HttpGet]
		public async Task<IActionResult> Roles(string userId)
		{
			if (userId == null)
			{
				return NotFound();
			}
			else
			{
				var user = await _userManager.FindByIdAsync(userId);

				var availableRoles = _roleManager.Roles.Select(r => new SelectListItem {
						Value = r.Name,
						Text = r.Name
						}).ToList();

				var model = new RolesViewModel() {
					Username = user.UserName,
					Id = userId,
					NewRole = availableRoles[0].Value,
					AvailableRoles = availableRoles

				};

				return View(model);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddRole(RolesViewModel model)
		{
			foreach(var a in ModelState)
			{
				foreach(var e in a.Value.Errors)
				{
					System.Console.WriteLine(e.ErrorMessage);
				}
			}

			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(model.Id);

				var result = await _userManager.AddToRoleAsync(user, model.NewRole);

				if (result.Succeeded)
				{
					return RedirectToAction("Edit", new { id = user.Id });
				}
				else
				{
				
				}
			}

			return View();
		}


	}
}
