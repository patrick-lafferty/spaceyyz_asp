using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaceYYZ.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class AdministratorController : Controller
	{
		public AdministratorController()
		{
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
