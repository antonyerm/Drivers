using Drivers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Drivers.Controllers
{
	public class DriversController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public DriversController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Add()
		{
			return Content("Adding a new driver");
		}




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}