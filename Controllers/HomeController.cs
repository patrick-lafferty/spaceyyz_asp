using System;
using Microsoft.AspNetCore.Mvc;

namespace SpaceYYZ.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			ViewData["ActiveTab"] = "Home";
			return View();
		}
	}
}
