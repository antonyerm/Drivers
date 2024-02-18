using System.ComponentModel.DataAnnotations;

namespace DriverServices.Models
{
	public class CarDto
	{
		public int Id { get; set; }

		public string Brand { get; set; }

		public string Model { get; set; }

		public string RegistrationNumber { get; set; }

		public virtual List<DriverDto> Drivers { get; set; }

    }
}

//Сущность автомобиля: Идентификатор (ключ, обязательное поле), Марка (обязательное поле), Модель, Номер (обязательное поле). Уникальный ключ: Номер
