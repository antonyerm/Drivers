using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class DriversRepository : IDriversRepository
	{
		DriversDbContext _context;
		public DriversRepository()
		{
			_context = new DriversDbContext();
			Seed();
		}

		private void Seed()
		{
			if (!_context.Drivers.Any())
			{
				var drivers = new List<Driver>()
				{
					new Driver()
					{
						Id = 1,
						FirstName = "John",
						LastName = "Lennon",
						MiddleName = "W.",
						Birthday = new DateTime(2012,12,05)
					},
					new Driver()
					{
						Id = 2,
						FirstName = "Paul",
						LastName = "McCartney",
						MiddleName = String.Empty,
						Birthday = new DateTime(1967,12,05)
					}
				};

				_context.Drivers.AddRange(drivers);
				_context.SaveChanges();
			}

			if (!_context.Cars.Any())
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

				_context.Cars.AddRange(cars);
				_context.SaveChanges();
			}
		}

		public Driver GetDriverById(int driverId)
		{
			var driver = _context.Drivers.FirstOrDefault(x => x.Id == driverId);
			return driver;
		}

		public List<Driver> GetDrivers()
		{
			var drivers = _context.Drivers;
			return drivers.ToList();
		}

		public Driver CreateDriver(Driver driver)
		{
			_context.Drivers.Add(driver);
			_context.SaveChanges();
			return driver;
		}

		public bool DeleteDriver(int driverId)
		{
			var driver = GetDriverById(driverId);
			if (driver != null)
			{
				_context.Drivers.Remove(driver);
				_context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public Driver UpdateDriver(Driver driver)
		{
			_context.Drivers.AddOrUpdate(driver);
			_context.SaveChanges();

			return driver;
		}

		public bool BatchDeleteCars()
		{
			var allCars = _context.Cars;
			_context.Cars.RemoveRange(allCars);
			_context.SaveChanges();

			return true;
		}

		public List<Car> GetCars()
		{
			var cars = _context.Cars;
			return cars.ToList();
		}

		public Car AssignCarToDriver(int driverId, int carId)
		{
			var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
			var driver = _context.Drivers.FirstOrDefault(d => d.Id == driverId);
			if (car == null || driver == null)
			{
				return null;
			}

			_context.Cars.Attach(car);
			_context.Drivers.Attach(driver);
			driver.Cars.Add(car);
			car.Drivers.Add(driver);

			_context.SaveChanges();

			return car;
		}

		public bool UnassignCarToDriver(int driverId, int carId)
		{
			var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
			var driver = _context.Drivers.FirstOrDefault(d => d.Id == driverId);
			if (car == null || driver == null)
			{
				return false;
			}

			_context.Cars.Attach(car);
			_context.Drivers.Attach(driver);
			driver.Cars.Remove(car);
			car.Drivers.Remove(driver);

			_context.SaveChanges();

			return true;
		}
	}
}
