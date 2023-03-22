using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiversideFishhut.API.Data
{
	public class Admin
	{
		[Key]
		public int AdminId { get; set; }

		[Required]
		public string AdminName { get; set; }

		[Required]
		public string AdminEmailAddress { get; set; }

		[Required]
		public string AdminPassword { get; set; }

		[Required]
		[ForeignKey(nameof(RoleId))]
		public int RoleId { get; set; }

		public Role Role { get; set; }
	}
}
