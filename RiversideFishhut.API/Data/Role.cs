using System.ComponentModel.DataAnnotations;

namespace RiversideFishhut.API.Data
{
	public class Role
	{
		[Key]
		public int RoleId { get; set; }

		[Required]
		public string RoleName { get; set; }
		public string RoleDescription { get; set; }
	}
}

