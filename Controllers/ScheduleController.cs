using System;
using Microsoft.AspNetCore.Mvc;

namespace SpaceYYZ.Controllers
{
	public class ScheduleController : Controller
	{
		public IActionResult Index()
		{
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
