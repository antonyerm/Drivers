using Drivers.Models;
using Drivers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class ReportsController : Controller
	{
		private readonly ILogger<ReportsController> _logger;
		private readonly ApiCommunications _communications;

		public ReportsController(ILogger<ReportsController> logger)
		{
			_logger = logger;
			_communications = new ApiCommunications("drivers");
		}

		public IActionResult Index()
		{
			var drivers = _communications.GetDrivers();
			return View(drivers);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}