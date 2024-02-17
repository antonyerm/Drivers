using System.ComponentModel.DataAnnotations;

namespace Drivers.Models
{
	public class Car
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Нет поля {0}.")]
		[Display(Name ="Марка")]
		public string Brand { get; set; }

		[Display(Name = "Модель")]
		public string? Model { get; set; }

		[Required(ErrorMessage = "Нет поля {0}.")]
		[Display(Name = "Номер")]
		public string RegistrationNumber { get; set; }

		public virtual ICollection<Driver>? Drivers { get; set; }

    }
}

//Сущность автомобиля: Идентификатор (ключ, обязательное поле), Марка (обязательное поле), Модель, Номер (обязательное поле). Уникальный ключ: Номер
