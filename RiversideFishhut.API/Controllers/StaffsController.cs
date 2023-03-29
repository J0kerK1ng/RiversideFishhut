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

		// GET: api/staff
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetAllStaff()
		{
			try
			{
				var staffList = await _context.staffs
					.Include(s => s.Role)
					.ToListAsync();

				if (staffList == null || staffList.Count == 0)
				{
					return NotFound(new CustomResponse(404, "No staff found", null));
				}

				var result = staffList.Select(staffMember => new
				{
					staffMember.StaffId,
					staffMember.StaffName,
					Role = new
					{
						staffMember.Role.RoleId,
						staffMember.Role.RoleName,
						staffMember.Role.RoleDescription
					},
					Email = staffMember.Email // Assuming you have added an Email property to the Staff model
				}).ToList();

				return new CustomResponse(200, "Staff list retrieved", result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}




		[HttpPost]
		public async Task<ActionResult<object>> CreateStaffMember(StaffCreationRequest request)
		{
			try
			{
				Staff newStaff = new Staff
				{
					StaffName = request.StaffName,
					Password = request.Password, // Don't forget to hash the password before storing it.
					RoleId = request.RoleId,
					Email = request.Email
				};

				_context.staffs.Add(newStaff);
				await _context.SaveChangesAsync();

				var response = new
				{
					Status = 200,
					Message = "Staff created successfully",
					Data = new
					{
						newStaff.StaffId,
						newStaff.StaffName,
						newStaff.Password, // Make sure not to return the actual password in a real-world application.
						newStaff.RoleId,
						newStaff.Email
					}
				};

				return StatusCode(200, response);
			}
			catch (Exception ex)
			{
				//return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
				return StatusCode(500, new CustomResponse(500, $"Internal Server Error: {ex.Message}. Inner exception: {ex.InnerException?.Message}", null));
			}
		}





		// PUT: api/staffs/password/{id}
		[HttpPut("{id}")]
		public async Task<ActionResult<object>> UpdateStaffMember(int id, StaffUpdateRequest request)
		{
			try
			{
				Staff staffToUpdate = await _context.staffs.FindAsync(id);

				if (staffToUpdate == null)
				{
					return NotFound(new CustomResponse(404, "Staff member not found", null));
				}

				staffToUpdate.StaffName = request.StaffName;
				staffToUpdate.Password = request.Password; // Don't forget to hash the password before updating it.
				staffToUpdate.RoleId = request.RoleId;
				staffToUpdate.Email = request.Email;

				await _context.SaveChangesAsync();

				var response = new
				{
					Status = 200,
					Message = "Staff updated successfully",
					Data = new
					{
						staffToUpdate.StaffId,
						staffToUpdate.StaffName,
						staffToUpdate.RoleId,
						staffToUpdate.Email
					}
				};

				return StatusCode(200, response);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}



		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteStaffMember(int id)
		{
			try
			{
				var staffMember = await _context.staffs.FindAsync(id);

				if (staffMember == null)
				{
					return NotFound(new CustomResponse(404, "Staff not found", null));
				}

				_context.staffs.Remove(staffMember);
				await _context.SaveChangesAsync();

				return new CustomResponse(200, "Staff deleted successfully", null);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

	}
}




