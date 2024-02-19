using Drivers.Models;
using Drivers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class DriversController : Controller
	{
		private readonly ILogger<DriversController> _logger;
		private readonly ApiCommunications _communications;

		public DriversController(ILogger<DriversController> logger)
		{
			_logger = logger;
			_communications = new ApiCommunications("drivers");
		}

		public IActionResult Index()
		{
			var drivers = _communications.GetDrivers();
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

			var newDriver = _communications.CreateDriver(driver);
			
			if (newDriver != null)
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
			var isDeletedSuccessfully = _communications.DeleteDriver(id);

			if (isDeletedSuccessfully)
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
		public IActionResult Update(int id)
		{
			var driver = _communications.GetDriverById(id);
			if (driver == null)
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция изменение водителя не выполнена.";
				return View("OperationComplete");
			}

			ViewBag.DriverId = id;
			return View(driver);
		}

		[HttpPost]
		public IActionResult Update(Driver driver)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var updatedDriver = _communications.UpdateDriver(driver);

			if (updatedDriver != null)
			{
				ViewBag.UserAlert = "Операция изменение водителя успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция изменение водителя не выполнена.";
			}
			
			return View("OperationComplete");
		}

		[HttpGet]
		public IActionResult AssignCar(int id)
		{
			_communications.ApiName = "cars";
			var allCars = _communications.GetCars();
			_communications.ApiName = "drivers";
			var driver = _communications.GetDriverById(id);
			var existingCarsOfDriver = driver.Cars.Select(x => x.Id);
			var cars = allCars.Where(x => !existingCarsOfDriver.Contains(x.Id)).ToList();

			var carAssignmentModel = new CarAssignmentModel();
			foreach (var car in cars)
			{
				carAssignmentModel.CarListOptions.Add(new SelectListItem(
					text: $"{car.RegistrationNumber} {car.Brand} {car.Model}",
					value: car.Id.ToString()
				));
			}

			ViewBag.DriverId = id;
			return View(carAssignmentModel);
		}

		[HttpPost]
		public IActionResult AssignCar(int driverId, int selectedCarId)
		{
			var assignedCar = _communications.AssignCar(driverId, selectedCarId);

			if (assignedCar != null)
			{
				ViewBag.UserAlert = "Операция прикрепления автомобиля водителю успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция прикрепления автомобиля водителю не выполнена.";
			}

			return View("OperationComplete");
		}

		[HttpGet]
		public IActionResult UnassignCar(int id)
		{
			var driverId = id;
			var driver = _communications.GetDriverById(id);
			var existingCarsOfDriver = driver.Cars;

			var carAssignmentModel = new CarAssignmentModel();
			foreach (var car in existingCarsOfDriver)
			{
				carAssignmentModel.CarListOptions.Add(new SelectListItem(
					text: $"{car.RegistrationNumber} {car.Brand} {car.Model}",
					value: car.Id.ToString()
				));
			}

			ViewBag.DriverId = driverId;
			return View(carAssignmentModel);
		}

		[HttpPost]
		public IActionResult UnassignCar(int driverId, int selectedCarId)
		{
			var isUnassignedSuccessfully = _communications.UnssignCar(driverId, selectedCarId);

			if (isUnassignedSuccessfully)
			{
				ViewBag.UserAlert = "Операция открепления автомобиля от водителя успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция открепления автомобиля от водителя не выполнена.";
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