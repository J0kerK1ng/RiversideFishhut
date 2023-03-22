using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/product-categories")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public CategoriesController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/Categories
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetCategories()
		{
			try
			{
				var categories = await _context.categories.Include(c => c.Products).ToListAsync();
				return new CustomResponse(200, "Successfully", categories);
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// GET: api/Categories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomResponse>> GetCategory(int id)
		{
			try
			{
				var category = await _context.categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == id);

				if (category == null)
				{
					return NotFound(new CustomResponse(404, "Not Found", null));
				}

				return new CustomResponse(200, "Successfully", category);
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// PUT: api/Categories/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> PutCategory(int id, Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest(new CustomResponse(400, "Bad Request", null));
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound(new CustomResponse(404, "Not Found", null));
				}
				else
				{
					throw;
				}
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}

			return new CustomResponse(200, "Successfully", null);
		}

		// POST: api/Categories
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostCategory(Category category)
		{
			try
			{
				_context.categories.Add(category);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}

			return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, new CustomResponse(200, "Successfully", category));
		}

		// DELETE: api/Categories/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteCategory(int id)
		{
			var category = await _context.categories.FindAsync(id);
			if (category == null)
			{
				return NotFound(new CustomResponse(404, "Not Found", null));
			}

			try
			{
				_context.categories.Remove(category);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}

			return new CustomResponse(200, "Successfully", null);
		}

		private bool CategoryExists(int id)
		{
			return _context.categories.Any(e => e.CategoryId == id);
		}
	}
}



