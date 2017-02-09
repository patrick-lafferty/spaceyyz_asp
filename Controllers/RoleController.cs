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
	public class RoleController : Controller
	{
		private readonly RoleManager<ApplicationRole> _roleManager;

		public RoleController(
				RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Roles()
		{

			var model = _roleManager.Roles.Select(r => new RoleViewModel {
					Name = r.Name,
					UsersAssigned = r.Users.Count,
					Description = r.Description
					});

			return View(model);
		}

		[HttpGet]
		public IActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (!await _roleManager.RoleExistsAsync(model.Name))
				{
					var role = new ApplicationRole() {
						Name = model.Name,
						Description = model.Description
					};

					var result = await _roleManager.CreateAsync(role);

					if (result.Succeeded)
					{
						return RedirectToAction("roles");
					}

				}

			}

			return BadRequest();
		}

	}
}
