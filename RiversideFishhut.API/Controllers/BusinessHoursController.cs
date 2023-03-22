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
		public async Task<ActionResult<CustomResponse>> GetBusinessHours()
		{
			try
			{
				var businessHours = await _context.businessHours
					.Select(b => new
					{
						b.BusinessHourId,
						b.DayOfWeek,
						b.BusinessTime
					})
					.ToListAsync();

				return new CustomResponse(200, "Business hours retrieved successfully", businessHours);
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// PUT: api/business-hours/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> PutBusinessHour(int id, BusinessHour businessHour)
		{
			if (id != businessHour.BusinessHourId)
			{
				return BadRequest(new CustomResponse(400, "Invalid record Id", null));
			}

			// only update the required fields
			_context.Entry(businessHour).Property(b => b.DayOfWeek).IsModified = true;
			_context.Entry(businessHour).Property(b => b.BusinessTime).IsModified = true;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!BusinessHourExists(id))
				{
					return NotFound(new CustomResponse(404, "Business hour not found", null));
				}
				else
				{
					throw;
				}
			}

			return new CustomResponse(200, "Business hour updated successfully", null);
		}

		private bool BusinessHourExists(int id)
		{
			return _context.businessHours.Any(e => e.BusinessHourId == id);
		}
	}
}

