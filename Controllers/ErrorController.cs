using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpaceYYZ.Controllers
{
	public class ErrorController : Controller
	{
		[Route("/error/{code}")]
		public IActionResult Index(int code)
		{
			System.Console.WriteLine(code);
			switch(code)
			{
				case 404:
					{
						return View("404");
					}
				default:
					{
						return View("Default");
					}
			}
		}
	}
}
