namespace RiversideFishhut.API.Data
{
	public class StaffCreationRequest
	{
		public int StaffId { get; set; }
		public string StaffName { get; set; }
		public string Password { get; set; }
		public int RoleId { get; set; }

		public string Email { get; set; }
	}
}
