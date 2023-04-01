using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RiversideFishhut.API.Data;
using static RiversideFishhut.API.Controllers.StaffsController;

namespace RiversideFishhut.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly RiversideFishhutDbContext _context;
		private readonly IConfiguration _configuration;

		public LoginsController(RiversideFishhutDbContext context, IConfiguration configuration)
		{
            _context = context; 
			_configuration = configuration;
		}


		// POST: api/auth/Login
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> Authenticate(LoginRequests loginRequest)
		{
			try
			{
				var staff = await _context.staffs.FirstOrDefaultAsync(s => s.StaffName == loginRequest.UserName);

				if (staff == null)
				{
					return NotFound(new CustomResponse(404, "User not found", null));
				}

				if (staff.Password == loginRequest.Password)
				{
					// Generate JWT token
					var token = GenerateJwtToken(staff, _configuration);

					var responseData = new
					{
						Token = token,
						User = new
						{
							UserId = staff.StaffId,
							UserName = staff.StaffName,
							Password = staff.Password
						}
					};

					return new CustomResponse(200, "Login successfully", responseData);
				}
				else
				{
					return new CustomResponse(401, "Invalid username or password", null);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// POST: api/auth/forget-password
		[HttpPost("forget-password")]
		public async Task<ActionResult<CustomResponse>> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
		{
			try
			{
				var staff = await _context.staffs.FirstOrDefaultAsync(s => s.StaffName == forgotPasswordRequest.UserName);

				if (staff == null)
				{
					return NotFound(new CustomResponse(404, "User not found", null));
				}

				string newPassword = "new_password";


				return new CustomResponse(200, "New password has been sent to your email", null);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
		private string GenerateJwtToken(Staff staff, IConfiguration configuration)
		{
			var jwtSettings = _configuration.GetSection("JWT");
			var issuer = jwtSettings["Issuer"];
			var audience = jwtSettings["Audience"];
			var key = jwtSettings["Key"];

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
		new Claim(JwtRegisteredClaimNames.Sub, staff.StaffName),
		new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
	};

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(60),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


	}


}

