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
    public class WebsiteInfoController : ControllerBase
    {
        private readonly RiversideFishhutDbContext _context;

        public WebsiteInfoController(RiversideFishhutDbContext context)
        {
            _context = context;
        }

        // GET: api/WebsiteInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebsiteInfo>>> GetwebsiteInfos()
        {
            //Select * from Countries
            var websiteInfo = await _context.websiteInfos.ToListAsync();
            return Ok(websiteInfo); 
        }

        // GET: api/WebsiteInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebsiteInfo>> GetWebsiteInfo(int id)
        {
            var websiteInfo = await _context.websiteInfos.FindAsync(id);

            if (websiteInfo == null)
            {
                return NotFound();
            }

            return Ok(websiteInfo);
        }

        // PUT: api/WebsiteInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsiteInfo(int id, WebsiteInfo websiteInfo)
        {
            if (id != websiteInfo.InfoId)
            {
                return BadRequest("Invalid record Id");
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WebsiteInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WebsiteInfo>> PostWebsiteInfo(WebsiteInfo websiteInfo)
        {
            _context.websiteInfos.Add(websiteInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebsiteInfo", new { id = websiteInfo.InfoId }, websiteInfo);
        }

        // DELETE: api/WebsiteInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsiteInfo(int id)
        {
            var websiteInfo = await _context.websiteInfos.FindAsync(id);
            if (websiteInfo == null)
            {
                return NotFound();
            }

            _context.websiteInfos.Remove(websiteInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebsiteInfoExists(int id)
        {
            return _context.websiteInfos.Any(e => e.InfoId == id);
        }
    }
}
