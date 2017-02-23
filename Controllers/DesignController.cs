using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaceYYZ.Controllers
{
	[Authorize(Roles = "Designer")]
	public class DesignController : Controller
	{
		public DesignController(
				)
		{
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Engines()
		{
			return View();
		}
	}
}
