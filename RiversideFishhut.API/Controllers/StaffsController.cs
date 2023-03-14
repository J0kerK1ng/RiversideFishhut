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

		// GET: api/staffs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<object>>> GetStaffs()
		{
			// return only the required fields using anonymous type
			return await _context.staffs
				.Select(s => new { s.StaffId, s.roleId, s.StaffName })
				.ToListAsync();
		}

		// GET: api/staff-roles
		[HttpGet("staff-roles")]
		public async Task<ActionResult<IEnumerable<object>>> GetStaffRoles()
		{
			// join the staffs and roles tables and select only the required fields using anonymous type
			return await _context.staffs
				.Join(_context.staffs, s => s.roleId, r => r.roleId, (s, r) => new { s.StaffId, s.StaffName, r.Description })
				.ToListAsync();
		}

		// POST: api/staff-roles
		[HttpPost("staff-roles")]
		public async Task<ActionResult<object>> PostStaffRole(Staff staff)
		{
			// add the new staff to the database and save changes
			_context.staffs.Add(staff);
			await _context.SaveChangesAsync();

			// retrieve the new staff with the role description and return only the required fields using anonymous type
			var result = await _context.staffs
				.Join(_context.staffs, s => s.roleId, r => r.roleId, (s, r) => new { s.StaffId, s.roleId, s.StaffName, r.Description })
				.SingleAsync(s => s.StaffId == staff.StaffId);

			return result;
		}

		// GET: api/staffs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<object>> GetStaff(int id)
		{
			// retrieve the staff with the given id and return only the required fields using anonymous type
			var staff = await _context.staffs
				.Select(s => new { s.StaffId, s.roleId, s.StaffName })
				.FirstOrDefaultAsync(s => s.StaffId == id);

			if (staff == null)
			{
				return NotFound();
			}

			return staff;
		}

		// PUT: api/staffs/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStaff(int id, Staff staff)
		{
			// retrieve the staff with the given id and update the StaffName and Password properties
			var existingStaff = await _context.staffs.FindAsync(id);
			if (existingStaff == null)
			{
				return NotFound();
			}

			existingStaff.StaffName = staff.StaffName;
			existingStaff.Password = staff.Password;

			_context.Entry(existingStaff).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StaffExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { status = "Success", message = $"Staff with id {id} updated successfully" });
		}

		// DELETE: api/staffs/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStaff(int id)
		{
			// retrieve the staff with the given id and remove it from the database
			var staff = await _context.staffs.FindAsync(id);
			if (staff == null)
			{
				return NotFound();
			}

			_context.staffs.Remove(staff);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool StaffExists(int id)
		{
			return _context.staffs.Any(e => e.StaffId == id);
		}
	}
}




