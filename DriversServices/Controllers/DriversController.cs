using AutoMapper;
using DriverServices.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace DriversServices.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DriversController : ControllerBase
	{
		private readonly ILogger<DriversController> _logger;
		private readonly IMapper _mapper;

		public DriversController(ILogger<DriversController> logger, IMapper mapper)
		{
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("{driverId}")]
		public DriverDto Get(int driverId)
		{
			var repository = new DriversRepository();
			var driver = repository.GetDriverById(driverId);
			var driverDto = _mapper.Map<DriverDto>(driver);

			return driverDto;
		}
	}
}