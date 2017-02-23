using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceYYZ.Data;
using SpaceYYZ.Models.DesignViewModels;

namespace SpaceYYZ.Controllers
{
	[Authorize(Roles = "Designer")]
	public class EngineController : Controller
	{
		private readonly DesignContext _context;

		public EngineController(DesignContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var model = _context.Engines
				.Select(e => new EngineViewModel() {
						Name = e.Name,
						SeaLevel = new Performance() {Thrust = e.SeaLevel.Thrust, Isp = e.SeaLevel.Isp},
						Vacuum = new Performance() {Thrust = e.Vacuum.Thrust, Isp = e.Vacuum.Isp}
					});

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		/*
		 * tests
		 * prototypes
		 * flight history
		 * */
	}
}
