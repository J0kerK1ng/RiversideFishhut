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

				var responseData = new
				{
					websiteInfo.StoreName,
					websiteInfo.LogoImage,
					websiteInfo.Description,
					websiteInfo.Address,
					websiteInfo.PhoneNumber,
					websiteInfo.OnlineOrderLink
				};

				return StatusCode(200, new CustomResponse(200, "Website info retrieved successfully", responseData));
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// POST: api/WebsiteInfo
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostWebsiteInfo(WebsiteInfo websiteInfo)
		{
			try
			{
				var existingWebsiteInfo = await _context.websiteInfos.FirstOrDefaultAsync();

				if (existingWebsiteInfo != null)
				{
					return StatusCode(400, new CustomResponse(400, "Website info already exists. Please update the existing info instead of creating a new one.", null));
				}

				_context.websiteInfos.Add(websiteInfo);
				await _context.SaveChangesAsync();

				var responseData = new
				{
					websiteInfo.StoreName,
					websiteInfo.LogoImage,
					websiteInfo.Description,
					websiteInfo.Address,
					websiteInfo.PhoneNumber,
					websiteInfo.OnlineOrderLink
				};

				return StatusCode(201, new CustomResponse(201, "Website info created successfully", responseData));
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
	}
}

