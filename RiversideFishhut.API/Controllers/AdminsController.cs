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
	public class AdminsController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public AdminsController(RiversideFishhutDbContext context)
		{
			_context = context;
		}
		// POST: api/admins/login
		[HttpPost("login")]
		public async Task<ActionResult<object>> LoginAdmin([FromBody] AdminLoginRequest request)
		{
			string adminName = request.AdminName;
			string adminPassword = request.AdminPassword;

			var admin = await _context.admins
				.Where(a => a.AdminName == adminName && a.AdminPassword == adminPassword)
				.Select(a => new
				{
					a.AdminId,
					a.AdminName,
					a.AdminEmailAddress,
					a.RoleId
				})
				.FirstOrDefaultAsync();

			if (admin == null)
			{
				return StatusCode(401, new { status = 401, message = "Invalid credentials" });
			}

			return StatusCode(200, new
			{
				status = 200,
				message = "Login successful",
				data = new
				{
					user = admin
				}
			});
		}
		// POST: api/auth/forgot-password
		[HttpPost("forgot-password")]
		public async Task<ActionResult<object>> ForgotPassword([FromBody] AdminForgotPasswordRequest request)
		{
			string adminName = request.AdminName;

			var admin = await _context.admins
				.Where(a => a.AdminName == adminName)
				.FirstOrDefaultAsync();

			if (admin == null)
			{
				return StatusCode(404, new { status = 404, message = "Admin not found" });
			}

			// Replace this line with the actual password reset email sending logic
			// SendPasswordResetEmail(admin.AdminEmailAddress);

			return StatusCode(200, new
			{
				Status = 200,
				Message = $"Password reset sent to {admin.AdminEmailAddress}",
				Data = (object)null
			});
		}
	}
}

