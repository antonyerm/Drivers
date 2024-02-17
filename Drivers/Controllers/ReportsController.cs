using Drivers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;

namespace Drivers.Controllers
{
	public class ReportsController : Controller
	{
		private readonly ILogger<ReportsController> _logger;

		public ReportsController(ILogger<ReportsController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			//TODO repository
			//var drivers = get all drivers and their cars
			
			// following is dummy code
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

			drivers[0].Cars = new List<Car>() { cars[0], cars[1] };
			drivers[1].Cars = new List<Car>() { cars[3], cars[4] };

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

		[HttpGet]
		public IActionResult AssignCar(int id)
		{
			var driverId = id;
			// TODO repository request
			var allCars = new List<Car>()
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
			// TODO repository request
			var existingCarsOfDriver = new List<int>() { 1, 2 };
			var cars = allCars.Where(x => !existingCarsOfDriver.Contains(x.Id)).ToList();

			var carAssignmentModel = new CarAssignmentModel();
			foreach (var car in cars)
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
		public IActionResult AssignCar(int driverId, int selectedCarId)
		{
			// DriversServices.AssignCarToDriver(id, id);
			// if response is 200, then 
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
			{
				ViewBag.UserAlert = "Операция добавления автомобиля водителю успешно выполнена.";
			}
			else
			{
				ViewBag.UserAlert = "Произошла ошибка. Операция добавления автомобиля водителю не выполнена.";
			}

			return View("OperationComplete");
		}

		[HttpGet]
		public IActionResult UnassignCar(int id)
		{
			var driverId = id;
			// TODO repository request
			var existingCarsOfDriver = new List<Car>()
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
				}
			};

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
			// DriversServices.UnassignCarToDriver(id, id);
			// if response is 200, then 
			var status = HttpStatusCode.OK;
			/////

			if (status == HttpStatusCode.OK)
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