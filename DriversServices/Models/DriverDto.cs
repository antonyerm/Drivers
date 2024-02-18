using System.ComponentModel.DataAnnotations;

namespace DriverServices.Models
{
	public class DriverDto
	{
		public int Id { get; set; }

		public string LastName{ get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public DateTime Birthday { get; set; }

		public virtual List<CarDto> Cars { get; set; }

    }
}

//Идентификатор (ключ, обязательное поле), Фамилия (обязательное поле), Имя (обязательное поле), Отчество, Дата рождения.  Уникальный ключ: Фамилия, Имя, Отчество, Дата рождения
