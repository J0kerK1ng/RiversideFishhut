namespace RiversideFishhut.API.Data
{
	public class StaffUpdateRequest
	{
		public string StaffName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public int RoleId { get; set; }
	}
}
