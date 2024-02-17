using System.ComponentModel.DataAnnotations;

namespace Drivers.Models
{
	public class Driver
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Нет поля {0}.")]
		[Display(Name = "Фамилия")]
		public string LastName{ get; set; }

		[Required(ErrorMessage = "Нет поля {0}.")]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Display(Name = "Отчество")]
		public string? MiddleName { get; set; }

		[Display(Name = "День рождения")]
		[DataType(DataType.Date)]
		public DateTime? Birthday { get; set; }

		public virtual ICollection<Car>? Cars { get; set; }

    }
}

//Идентификатор (ключ, обязательное поле), Фамилия (обязательное поле), Имя (обязательное поле), Отчество, Дата рождения.  Уникальный ключ: Фамилия, Имя, Отчество, Дата рождения
