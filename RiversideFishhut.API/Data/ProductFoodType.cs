namespace RiversideFishhut.API.Data
{
	public class ProductFoodType
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public int TypeId { get; set; }
		public FoodType FoodType { get; set; }
	}
}
