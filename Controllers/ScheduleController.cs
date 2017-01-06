using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpaceYYZ.Controllers
{
	[Authorize]
	public class ScheduleController : Controller
	{
		public IActionResult Index()
		{
			ViewData["ActiveTab"] = "Schedule";
			return View();
		}

		public IActionResult FlightStatus()
		{
			return View();
		}

		public IActionResult NewFlight()
		{
			return View();
		}
	}
}
