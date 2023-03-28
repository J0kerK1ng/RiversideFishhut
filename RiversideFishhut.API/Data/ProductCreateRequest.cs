using RiversideFishhut.API.Data;

public class ProductCreateRequest
{
	public string ProductName { get; set; }
	public string AltName { get; set; }
	public string Description { get; set; }
	public decimal Dine_in_price { get; set; }
	public decimal Take_out_price { get; set; }
	public List<FoodTypeRequest> FoodTypes { get; set; } // Use a simplified FoodTypeRequest class
	public int? CategoryId { get; set; } // Nullable CategoryId property
}






