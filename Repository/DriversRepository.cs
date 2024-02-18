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
			List<Car> cars = new List<Car>();
			List<Driver> drivers = new List<Driver>();

			if (!_context.Drivers.Any())
			{
				drivers.Add(
					new Driver()
					{
						Id = 1,
						FirstName = "John",
						LastName = "Lennon",
						MiddleName = "W.",
						Birthday = new DateTime(2012, 12, 05)
					});
				drivers.Add(
					new Driver()
					{
						Id = 2,
						FirstName = "Paul",
						LastName = "McCartney",
						MiddleName = String.Empty,
						Birthday = new DateTime(1967, 12, 05)
					});

				_context.Drivers.AddRange(drivers);
				_context.SaveChanges();
			}

			if (!_context.Cars.Any())
			{
				cars.Add(
					new Car()
					{
						Id = 1,
						Brand = "Kia",
						Model = "Rio",
						RegistrationNumber = "02HGA215"
					});
				cars.Add(
					new Car()
					{
						Id = 2,
						Brand = "Audi",
						Model = "100",
						RegistrationNumber = "01ABC365"
					});
				cars.Add(
					new Car()
					{
						Id = 3,
						Brand = "Toyota",
						Model = "Camry",
						RegistrationNumber = "09TGA918"
					});
				cars.Add(
					new Car()
					{
						Id = 4,
						Brand = "Renault",
						Model = "Logan",
						RegistrationNumber = "13BCD846"
					});
				cars.Add(
					new Car()
					{
						Id = 5,
						Brand = "Audi",
						Model = "100",
						RegistrationNumber = "01DHA498"
					});

				_context.Cars.AddRange(cars);
				_context.SaveChanges();
			};


			if (!_context.DriverCar.Any())
			{
				var driver1 = _context.Drivers.First();
				driver1.Cars = new List<Car>();
				var driver2 = _context.Drivers.OrderBy(d => d.Id).Skip(1).First();
				driver2.Cars = new List<Car>();

				for (int i = 0; i < cars.Count; i++)
				{
					if (i < 2)
					{
						driver1.Cars.Add(cars[i]);
					}
					else
					{
						driver2.Cars.Add(cars[i]);
					}
				}

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
			var drivers = _context.Drivers.Include("Cars");
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
			var existingDriver = _context.Drivers.Find(driver.Id);
			if (existingDriver != null)
			{
				existingDriver.FirstName = driver.FirstName;
				existingDriver.LastName = driver.LastName;
				existingDriver.MiddleName = driver.MiddleName;
				existingDriver.Birthday = driver.Birthday;

				_context.SaveChanges();
			}
			
			return existingDriver;
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
