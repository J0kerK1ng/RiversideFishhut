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
	[Route("api/business-hours")]
	[ApiController]
	public class BusinessHoursController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public BusinessHoursController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/business-hours
		[HttpGet]
		public async Task<ActionResult<IEnumerable<object>>> GetBusinessHours()
		{
			// return only the required fields using anonymous type
			return await _context.businessHours
				.Select(b => new { b.BusinessHourId, b.dayOfWeek, b.businessTime })
				.ToListAsync();
		}

		// GET: api/business-hours/5
		[HttpGet("{id}")]
		public async Task<ActionResult<BusinessHour>> GetBusinessHour(int id)
		{
			var businessHour = await _context.businessHours.FindAsync(id);

			if (businessHour == null)
			{
				return NotFound();
			}

			// return the full BusinessHour object
			return businessHour;
		}

		// PUT: api/business-hours/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBusinessHour(int id, BusinessHour businessHour)
		{
			if (id != businessHour.BusinessHourId)
			{
				return BadRequest();
			}

			// only update the required fields
			_context.Entry(businessHour).Property(b => b.dayOfWeek).IsModified = true;
			_context.Entry(businessHour).Property(b => b.businessTime).IsModified = true;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BusinessHourExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			// return only the updated fields
			return Ok(new { businessHour.dayOfWeek, businessHour.businessTime });
		}

		// POST: api/business-hours
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<BusinessHour>> PostBusinessHour(BusinessHour businessHour)
		{
			_context.businessHours.Add(businessHour);
			await _context.SaveChangesAsync();

			// return only the required fields
			return CreatedAtAction(nameof(GetBusinessHour), new { id = businessHour.BusinessHourId },
				new { businessHour.BusinessHourId, businessHour.dayOfWeek, businessHour.businessTime });
		}

		// DELETE: api/business-hours/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBusinessHour(int id)
		{
			var businessHour = await _context.businessHours.FindAsync(id);
			if (businessHour == null)
			{
				return NotFound();
			}

			_context.businessHours.Remove(businessHour);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BusinessHourExists(int id)
		{
			return _context.businessHours.Any(e => e.BusinessHourId == id);
		}

	}
}

