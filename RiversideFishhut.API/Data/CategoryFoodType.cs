namespace RiversideFishhut.API.Data
{
	public class CategoryFoodType
	{
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public int FoodTypeId { get; set; }
		public FoodType FoodType { get; set; }
	}

}
