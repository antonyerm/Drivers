using Drivers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class CarsController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public CarsController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			var cars = new List<Car>()
			{
				new Car()
				{
					Id = 1,
					Brand = "Kia",
					Model = "Rio",
					RegistrationNumber = "02HGA215"
				},
				new Car()
				{
					Id = 2,
					Brand = "Audi",
					Model = "100",
					RegistrationNumber = "01ABC365"
				},
				new Car()
				{
					Id = 3,
					Brand = "Toyota",
					Model = "Camry",
					RegistrationNumber = "09TGA918"
				},
				new Car()
				{
					Id = 4,
					Brand = "Renault",
					Model = "Logan",
					RegistrationNumber = "13BCD846"
				},
				new Car()
				{
					Id = 5,
					Brand = "Audi",
					Model = "100",
					RegistrationNumber = "01DHA498"
				}
			};

			return View(cars);
		}

		public IActionResult BatchDelete()
		{
			// DriversServices.CarsBatchDelete()
			// if respond is 200
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
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