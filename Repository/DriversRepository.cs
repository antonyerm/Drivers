using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class DriversRepository
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

		public Driver GetDriverFirstOrDefault(int id)
		{
			var driver = _context.Drivers.FirstOrDefault();
			return driver;
		}

		public Driver GetDriverById(int id)
		{
			var driver = _context.Drivers.FirstOrDefault(x => x.Id == id);
			return driver;
		}
	}
}
