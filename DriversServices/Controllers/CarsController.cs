using AutoMapper;
using DriverServices.Models;
using DriversServices.Configuration;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;

namespace DriversServices.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CarsController : ControllerBase
	{
		private readonly ILogger<DriversController> _logger;
		private readonly IMapper _mapper;
		private readonly IDriversRepository _driversRepository;

		public CarsController(ILogger<DriversController> logger, 
								 IMapper mapper,
								 IDriversRepository driversRepository)
		{
			_logger = logger;
			_mapper = mapper;
			_driversRepository = driversRepository;
		}

		[HttpDelete]
		public ActionResult BatchDeleteCars()
		{
			var isSuccess = _driversRepository.BatchDeleteCars();
			if (isSuccess)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
	}
}