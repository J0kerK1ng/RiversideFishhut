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
	[Route("api/website-info")]
	[ApiController]
	public class WebsiteInfoController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public WebsiteInfoController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/WebsiteInfo
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetWebsiteInfo()
		{
			try
			{
				var websiteInfo = await _context.websiteInfos.FirstOrDefaultAsync();

				if (websiteInfo == null)
				{
					return StatusCode(404, new CustomResponse(404, "Website info not found", null));
				}

				return StatusCode(200, new CustomResponse(200, "Website info retrieved successfully", websiteInfo));
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// PUT: api/WebsiteInfo/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> PutWebsiteInfo(int id, WebsiteInfo websiteInfo)
		{
			if (id != websiteInfo.InfoId)
			{
				return BadRequest(new CustomResponse(400, "Invalid record Id", null));
			}

			_context.Entry(websiteInfo).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!WebsiteInfoExists(id))
				{
					return StatusCode(404, new CustomResponse(404, "Website info not found", null));
				}
				else
				{
					throw;
				}
			}

			return StatusCode(200, new CustomResponse(200, "Website info updated successfully", null));
		}

		private bool WebsiteInfoExists(int id)
		{
			return _context.websiteInfos.Any(e => e.InfoId == id);
		}
	}
}
