using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
	public class OrderType
	{
		[Key]
		public int OrderTypeId { get; set; }

		public string TypeName { get; set; }
	}
}
