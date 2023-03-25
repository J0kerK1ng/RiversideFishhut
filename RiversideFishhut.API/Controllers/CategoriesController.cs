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
				var categories = await _context.categories.ToListAsync();

				var result = categories.Select(c => new
				{
					c.CategoryId,
					c.Name,
					c.Description
				}).ToList();

				return new CustomResponse(200, "Successfully retrieved categories", result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}


		// PUT: api/ProductCategories/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest)
		{
			try
			{
				var category = await _context.categories.FindAsync(id);

				if (category == null)
				{
					return NotFound(new CustomResponse(404, "Category not found", null));
				}

				category.Name = updateCategoryRequest.Name;
				category.Description = updateCategoryRequest.Description;

				await _context.SaveChangesAsync();

				return new CustomResponse(200, "Product category updated successfully", null);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// POST: api/Categories
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostCategory(CategoryCreateRequest categoryCreateRequest)
		{
			try
			{
				Category category = new Category
				{
					Name = categoryCreateRequest.Name,
					Description = categoryCreateRequest.Description
				};

				_context.categories.Add(category);
				await _context.SaveChangesAsync();

				var categories = await _context.categories
					.Select(c => new { c.CategoryId, c.Name, c.Description })
					.ToListAsync();

				return CreatedAtAction(nameof(GetCategories), new { id = category.CategoryId }, new CustomResponse(201, "Product category created successfully", categories));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
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



