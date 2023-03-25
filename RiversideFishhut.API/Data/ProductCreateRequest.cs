namespace RiversideFishhut.API.Data
{
	public class ProductCreateRequest
	{
		public string ProductName { get; set; }
		public string AltName { get; set; }
		public decimal Dine_in_price { get; set; }
		public decimal Take_out_price { get; set; }

		public IList<FoodTypeCreateRequest> FoodTypes { get; set; }
		public CategoryCreateRequest Category { get; set; }
	}
}
