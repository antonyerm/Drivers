using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drivers.Models
{
	public class CarAssignmentModel
	{
		public int DriverId { get; set; }
		
		public int SelectedCarId { get; set; }

		public List<SelectListItem> CarListOptions { get; set; } = new List<SelectListItem>();
	}
}
