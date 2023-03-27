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

		// GET: api/product-categories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomResponse>> GetCategory(int id)
		{
			var category = await _context.categories.FindAsync(id);

			if (category == null)
			{
				return NotFound(new CustomResponse(404, "Category not found", null));
			}

			var result = new
			{
				category.CategoryId,
				category.Name,
				category.Description
			};

			return new CustomResponse(200, "Successfully retrieved category", result);
		}

		
		// POST: api/product-categories
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostCategory(CreateProductCategoryRequest createRequest)
		{
			var category = new Category
			{
				Name = createRequest.Name,
				Description = createRequest.Description
			};

			_context.categories.Add(category);
			await _context.SaveChangesAsync();

			var data = new
			{
				category.CategoryId,
				category.Name,
				category.Description
			};

			return new CustomResponse(201, "Product category created successfully", data);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, CategoryUpdateRequest categoryUpdateRequest)
		{
			if (!CategoryExists(id))
			{
				return NotFound();
			}

			var categoryToUpdate = await _context.categories.FindAsync(id);

			if (categoryToUpdate == null)
			{
				return NotFound();
			}

			categoryToUpdate.Name = categoryUpdateRequest.Name;
			categoryToUpdate.Description = categoryUpdateRequest.Description;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			var responseData = new
			{
				categoryToUpdate.CategoryId,
				categoryToUpdate.Name,
				categoryToUpdate.Description
			};

			return StatusCode(200, new CustomResponse(200, "Update successfully", responseData));
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
			return _context.categories.Any(c => c.CategoryId == id);
		}
	}
}







