using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		public string ProductName { get; set; }

		public string AltName { get; set; }

		public decimal Dine_in_price { get; set; }
		public decimal Take_out_price { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public int CategoryId { get; set; }

		public Category Category { get; set; }

		public virtual ICollection<FoodType> FoodTypes { get; set; }

		public Product()
		{
			FoodTypes = new List<FoodType>();
		}
	}
}




