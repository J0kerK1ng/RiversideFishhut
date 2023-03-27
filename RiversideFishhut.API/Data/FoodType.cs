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

		// Removed ProductId and Product properties

		public ICollection<ProductFoodType> ProductFoodTypes { get; set; }
	}
}

