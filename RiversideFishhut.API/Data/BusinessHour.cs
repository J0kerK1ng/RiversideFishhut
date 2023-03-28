using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class BusinessHour
	{
		[Key]
		public int BusinessHourId { get; set; }
		public string DayOfWeek { get; set; }
		public string BusinessTime { get; set; }

		
		
	}
}
