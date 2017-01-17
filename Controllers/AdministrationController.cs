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
		public IActionResult Index(string sort, string search)
		{

			ViewData["UsernameSort"] = sort == "username" ? "username_desc" : "username"; 
			ViewData["FirstnameSort"] = sort == "firstname" ? "firstname_desc" : "firstname";
			ViewData["LastnameSort"] = sort == "lastname" ? "lastname_desc" : "lastname";
			ViewData["Sort"] = sort;
			ViewData["PrevSearch"] = search;

			var model = _userManager.Users
				.Where(u => string.IsNullOrEmpty(search)
						|| u.UserName.Contains(search)
						|| u.FirstName.Contains(search)
						|| u.LastName.Contains(search))
				.Select(u => new UserViewModel() { 
					Id = u.Id,
					Username = u.UserName,
					FirstName = u.FirstName,
					LastName = u.LastName,

					});

			switch(sort)
			{

				case "username_desc":
					{
						model = model.OrderByDescending(u => u.Username);
						break;
					}
				case "firstname":
					{
						model = model.OrderBy(u => u.FirstName);
						break;
					}
				case "firstname_desc":
					{
						model = model.OrderByDescending(u => u.FirstName);
						break;
					}
				case "lastname":
					{
						model = model.OrderBy(u => u.LastName);
						break;
					}
				case "lastname_desc":
					{
						model = model.OrderByDescending(u => u.LastName);
						break;
					}
				case "username":
				default:
					{
						model = model.OrderBy(u => u.Username);
						break;
					}
			}


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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UserDetailsViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(model.Id);
				if (user != null)
				{
					if (await TryUpdateModelAsync<ApplicationUser>(user, 
							"",
							u => u.FirstName, u => u.LastName))
					{
						var result = await _userManager.UpdateAsync(user);
						
						if (result.Succeeded)
						{
							return RedirectToAction("Details", new {id = user.Id});
						}

					}

				}
			}
			else
			{
				PrintModelErrors();

			}

			return RedirectToAction("Edit", new {id = model.Id});
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

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (id != null)
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user != null)
				{

					var model = new DeleteUserViewModel() {
						Username = user.UserName,
						Id = id
					};

					return View(model);
				}
			}

			return BadRequest();
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(string id)
		{
			if (id != null)
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user != null)
				{
					var result = await _userManager.DeleteAsync(user);

					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}
				}
			}

			return BadRequest();
		}

		private void PrintModelErrors()
		{
			foreach(var a in ModelState)
			{
				foreach(var e in a.Value.Errors)
				{
					System.Console.WriteLine(e.ErrorMessage);
				}
			}
		}
	}
}
