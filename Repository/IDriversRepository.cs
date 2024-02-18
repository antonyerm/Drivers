using Repository.Models;

namespace Repository
{
	public interface IDriversRepository
	{
		public Driver GetDriverById(int driverId);

		public List<Driver> GetDrivers();

		public Driver CreateDriver(Driver driver);

		public bool DeleteDriver(int driverId);

		public Driver UpdateDriver(Driver driver);

		public bool BatchDeleteCars();

		public List<Car> GetCars();


		public Car AssignCarToDriver(int driverId, int carId);

		public bool UnassignCarToDriver(int driverId, int carId);
	}
}
