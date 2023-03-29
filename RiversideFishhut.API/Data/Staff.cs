using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class Staff
	{
		[Key]
		public int StaffId { get; set; }

		[Required]
		public string StaffName { get; set; }

		[Required]
		public string Password { get; set; }

		public string Email { get; set; }

		[Required]
		[ForeignKey(nameof(RoleId))]
		public int RoleId { get; set; }

		public Role Role { get; set; }
	}
}

