using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		public string Name { get; set; }

		public string Description { get; set; }

		public virtual IList<Product> Products { get; set; }

		public Category()
		{
			Products = new List<Product>();
		}
	}
}
