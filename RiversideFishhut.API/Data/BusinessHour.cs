using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class BusinessHour
	{
		[Key]
		public int BusinessHourId { get; set; }
		public string dayOfWeek { get; set; }
		public string businessTime { get; set; }

		
		
	}
}
