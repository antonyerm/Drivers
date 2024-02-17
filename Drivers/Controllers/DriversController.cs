using Drivers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class DriversController : Controller
	{
		private readonly ILogger<DriversController> _logger;

		public DriversController(ILogger<DriversController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			var drivers = new List<Driver>()
			{
				new Driver()
				{
					Id = 1,
					FirstName = "John",
					LastName = "Smith",
					MiddleName = "K.",
					Birthday = new DateTime(2012,12,05)
				},
				new Driver()
				{
					Id = 2,
					FirstName = "Paul",
					LastName = "McCarney",
					MiddleName = "K.",
					Birthday = new DateTime(1967,12,05)
				}
			};

			return View(drivers);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Driver driver)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			// DriversServices.AddDriver(driver);
			// if response is 200, then 
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
			{
				ViewBag.UserAlert = "Операция добавление водителя успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция удаление водителя не выполнена.";
			}

			return View("OperationComplete");
		}

		public IActionResult Delete(int id)
		{
			// DriversServices.DeleteDriver(id);
			// if response is 200, then 
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
			{
				ViewBag.UserAlert = "Операция удаление водителя успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция удаление водителя не выполнена.";
			}

			return View("OperationComplete");
		}

		[HttpGet]
		public IActionResult Edit(Driver driver)
		{
			// DriversServices.UpdateDriver(driver);
			// if response is 200, then 
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
			{
				ViewBag.UserAlert = "Операция изменение водителя успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция изменение водителя не выполнена.";
			}
			
			return View("OperationComplete");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}