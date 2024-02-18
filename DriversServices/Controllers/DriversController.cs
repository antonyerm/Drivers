using AutoMapper;
using DriverServices.Models;
using DriversServices.Configuration;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;

namespace DriversServices.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DriversController : ControllerBase
	{
		private readonly ILogger<DriversController> _logger;
		private readonly IMapper _mapper;
		private readonly IDriversRepository _driversRepository;

		public DriversController(ILogger<DriversController> logger, 
								 IMapper mapper,
								 IDriversRepository driversRepository)
		{
			_logger = logger;
			_mapper = mapper;
			_driversRepository = driversRepository;
		}

		[HttpGet]
		[Route("{driverId}")]
		public ActionResult<DriverDto> Get(int driverId)
		{
			var driver = _driversRepository.GetDriverById(driverId);
			var driverDto = _mapper.Map<DriverDto>(driver);

			if (driverDto == null)
			{
				return NotFound();
			}

			return Ok(driverDto);
		}

		[HttpGet]
		[Route("")]
		public ActionResult<List<DriverDto>> Get()
		{
			var allDrivers = _driversRepository.GetDrivers();
			List<DriverDto> driversDto = null;

			foreach (var driver in allDrivers)
			{
				if (driversDto == null)
				{
					driversDto = new List<DriverDto>();
				}
				
				driversDto.Add(_mapper.Map<DriverDto>(driver));

			}

			if (driversDto == null)
			{
				return NotFound();
			}

			return Ok(driversDto);
		}

		[HttpPost]
		[Route("")]
		public ActionResult<Driver> Create([FromBody] DriverDto driver)
		{
			var newDriver = _driversRepository.CreateDriver(_mapper.Map<Driver>(driver));

			if (newDriver == null)
			{
				return NotFound();
			}

			return Ok(newDriver);
		}

		[HttpDelete]
		[Route("{id}")]
		public ActionResult DeleteDriver(int id)
		{
			var isSuccess = _driversRepository.DeleteDriver(id);
			if (isSuccess)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}

		[HttpPut]
		public ActionResult<Driver> UpdateDriver(Driver driver)
		{
			var updatedDriver = _driversRepository.UpdateDriver(driver);
			if (updatedDriver != null)
			{
				return Ok(updatedDriver);
			}
			else
			{
				return NotFound();
			}
		}

		//[HttpPut]
		//public ActionResult<Car> AssignCar(int driverId, int carId)
		//{
		//	var car = _driversRepository.AssignCarToDriver(driverId, carId);
		//	if (car != null)
		//	{
		//		return Ok(car);
		//	}
		//	else
		//	{
		//		return NotFound();
		//	}
		//}

		//[HttpPut]
		//public ActionResult<Car> UnassignCar(int driverId, int carId)
		//{
		//	var isSuccess = _driversRepository.UnassignCarToDriver(driverId, carId);
		//	if (isSuccess)
		//	{
		//		return Ok();
		//	}
		//	else
		//	{
		//		return NotFound();
		//	}
		//}
	}
}