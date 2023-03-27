namespace RiversideFishhut.API.Data
{
	public class ProductUpdateDTO
	{
		public string ProductName { get; set; }
		public string AltName { get; set; }
		public string Description { get; set; }
		public decimal Dine_in_price { get; set; }
		public decimal Take_out_price { get; set; }
		public List<FoodType> FoodTypes { get; set; }
		public Category Category { get; set; }
	}
}
