﻿using System.Collections.Generic;
namespace RiversideFishhut.API.Data
{
	public class UpdateProductRequest
	{
		public string ProductName { get; set; }
		public string AltName { get; set; }
		public string Description { get; set; }
		public decimal Dine_in_price { get; set; }
		public decimal Take_out_price { get; set; }
		public List<int> FoodTypes { get; set; } // Replace List<FoodType> with List<int>
		public int? CategoryId { get; set; } // Replace Category with CategoryId
	}
}
