using Drivers.Models;
using Drivers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class CarsController : Controller
	{
		private readonly ILogger<CarsController> _logger;
		private readonly ApiCommunications _communications;

		public CarsController(ILogger<CarsController> logger)
		{
			_logger = logger;
			_communications = new ApiCommunications("cars");
		}

		public IActionResult Index()
		{
			var cars = _communications.GetCars();

			return View(cars);
		}

		public IActionResult BatchDelete()
		{
			var areDeletedSuccessfull = _communications.BatchDeleteCars();

			if (areDeletedSuccessfull)
			{
				ViewBag.UserAlert = "Операция удаление всех машин успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция удаление всех машин не выполнена.";
			}

			return View("OperationComplete");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}