using AutoMapper;
using DriverServices.Models;
using Repository.Models;

namespace DriversServices.Configuration
{
	public class MapperConfiguration : Profile
	{
		public MapperConfiguration()
		{
			CreateMap<Driver,
				DriverDto>().ReverseMap();
			CreateMap<Car,
				CarDto>().ReverseMap();
		}
	}
}
