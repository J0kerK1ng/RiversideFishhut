namespace RiversideFishhut.API.Data
{
	public class OrderCreateRequest
	{
		public decimal BeforeTax { get; set; }
		public List<FoodItemRequest> Food { get; set; }
		public string Note { get; set; }
		public string OrderType { get; set; }
		public CustomerCreateRequest Customer { get; set; }
		public int Table { get; set; }
		public decimal Tax { get; set; }
		public decimal TotalCost { get; set; }
		public int? StaffId { get; set; }
	}
}
