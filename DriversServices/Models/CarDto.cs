using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriverServices.Models
{
	public class CarDto
	{
		public int Id { get; set; }

		public string Brand { get; set; }

		public string Model { get; set; }

		public string RegistrationNumber { get; set; }

		[JsonIgnore]
		public virtual List<DriverDto>? Drivers { get; set; }

    }
}

//Сущность автомобиля: Идентификатор (ключ, обязательное поле), Марка (обязательное поле), Модель, Номер (обязательное поле). Уникальный ключ: Номер
