using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/website-info")]
	[ApiController]
	[Authorize]
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
					websiteInfo.InfoId,
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
		// POST: api/website-update
		[HttpPost("website-update")]
		public async Task<ActionResult<CustomResponse>> PostWebsiteInfo(UpdateWebsiteInfoRequest updateWebsiteInfoRequest)
		{
			try
			{
				var websiteInfo = await _context.websiteInfos.FirstOrDefaultAsync();

				if (websiteInfo == null)
				{
					// Create a new website info if not exists
					websiteInfo = new WebsiteInfo();
					_context.websiteInfos.Add(websiteInfo);
				}

				websiteInfo.StoreName = updateWebsiteInfoRequest.StoreName;
				websiteInfo.LogoImage = updateWebsiteInfoRequest.LogoImage;
				websiteInfo.Description = updateWebsiteInfoRequest.Description;
				websiteInfo.Address = updateWebsiteInfoRequest.Address;
				websiteInfo.PhoneNumber = updateWebsiteInfoRequest.PhoneNumber;
				websiteInfo.OnlineOrderLink = updateWebsiteInfoRequest.OnlineOrderLink;

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

				return StatusCode(200, new CustomResponse(200, "Website info updated successfully", responseData));
			}
			catch (Exception ex)
			{
				// You can log the exception here if needed
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
	}
}





