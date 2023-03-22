using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class FoodType
	{
		[Key]
		public int TypeId { get; set; }
		public string TypeName { get; set; }
		public string Description { get; set; }

		[ForeignKey(nameof(ProductId))]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}

