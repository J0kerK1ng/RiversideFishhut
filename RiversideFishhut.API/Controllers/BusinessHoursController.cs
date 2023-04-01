using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/business-hours")]
	[ApiController]
	[Authorize]
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
						BusinessHourId = b.BusinessHourId.ToString(),
						DayOfWeek = b.DayOfWeek,
						BusinessTime = b.BusinessTime,
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

		// PUT: api/business-hours/{id}
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> UpdateBusinessHour(int id, UpdateBusinessHourRequest updateBusinessHourRequest)
		{
			try
			{
				var businessHour = await _context.businessHours.FindAsync(id);

				if (businessHour == null)
				{
					return NotFound(new CustomResponse(404, "Business hour not found", null));
				}

				businessHour.DayOfWeek = updateBusinessHourRequest.DayOfWeek;
				businessHour.BusinessTime = updateBusinessHourRequest.BusinessTime;

				await _context.SaveChangesAsync();

				var responseData = new
				{
					businessHour.BusinessHourId,
					businessHour.DayOfWeek,
					businessHour.BusinessTime
				};

				return StatusCode(200, new CustomResponse(200, "Business hour updated successfully", responseData));
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

	}
}

