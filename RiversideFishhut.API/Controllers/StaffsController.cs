using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StaffsController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public StaffsController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		public class LoginRequest
		{
			public string StaffName { get; set; }
			public string Password { get; set; }
		}

		public class PasswordChangeRequest
		{
			public string NewPassword { get; set; }
		}

		private bool StaffExists(int id)
		{
			return _context.staffs.Any(e => e.StaffId == id);
		}

		// POST: api/staffs/login
		[HttpPost("login")]
		public async Task<ActionResult<object>> LoginStaff([FromBody] LoginRequest loginRequest)
		{
			var staff = await _context.staffs
				.Where(s => s.StaffName == loginRequest.StaffName && s.Password == loginRequest.Password)
				.Select(s => new
				{
					s.StaffId,
					s.RoleId,
					s.StaffName
				})
				.FirstOrDefaultAsync();

			if (staff == null)
			{
				return StatusCode(401, new { status = 401, message = "Invalid credentials" });
			}

			return StatusCode(200, new
			{
				status = 200,
				message = "Login successful",
				staff = new[] { staff }
			});
		}

		// PUT: api/staffs/password/{id}
		[HttpPut("password/{id}")]
		public async Task<ActionResult<object>> ChangePassword(int id, [FromBody] PasswordChangeRequest passwordChangeRequest)
		{
			var staff = await _context.staffs.FindAsync(id);

			if (staff == null)
			{
				return NotFound(new { status = 404, message = "Staff not found" });
			}

			staff.Password = passwordChangeRequest.NewPassword;
			_context.Entry(staff).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StaffExists(id))
				{
					return NotFound(new { status = 404, message = "Staff not found" });
				}
				else
				{
					throw;
				}
			}

			return StatusCode(200, new
			{
				status = 200,
				message = "Password changed successfully",
				data = (object)null
			});
		}

		

		
	}
}




