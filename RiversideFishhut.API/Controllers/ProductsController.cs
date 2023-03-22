using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public ProductsController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		private bool ProductExists(int id)
		{
			return _context.products.Any(e => e.ProductId == id);
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> Getproducts()
		{
			try
			{
				var products = await _context.products
					.Include(p => p.Category)
					.Include(p => p.FoodTypes)
					.Select(p => new
					{
						p.ProductId,
						p.ProductName,
						p.Dine_in_price,
						p.Take_out_price,
						FoodTypes = p.FoodTypes.Select(ft => ft.TypeName).ToList(),
						p.CategoryId
					})
					.ToListAsync();

				return new CustomResponse(200, "Successfully", products);
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// POST: api/Products
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostProduct([FromBody] Product product)
		{
			try
			{
				var category = await _context.categories.FindAsync(product.Category.CategoryId);
				if (category == null)
				{
					category = new Category
					{
						CategoryId = product.Category.CategoryId,
						Name = product.Category.Name,
						Description = product.Category.Description
					};
					_context.categories.Add(category);
				}

				product.CategoryId = category.CategoryId;

				_context.products.Add(product);
				await _context.SaveChangesAsync();

				var responseData = new
				{
					product.ProductId,
					product.ProductName,
					product.AltName,
					product.Dine_in_price,
					product.Take_out_price,
					FoodTypes = product.FoodTypes.Select(ft => new
					{
						ft.TypeId,
						ft.TypeName,
						ft.Description
					}),
					Category = new
					{
						product.Category.CategoryId,
						product.Category.Name,
						product.Category.Description
					}
				};

				return StatusCode(201, new CustomResponse(201, "Successful", responseData));
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> PutProduct(int id, [FromBody] Product updatedProduct)
		{
			try
			{
				var product = await _context.products.Include(p => p.FoodTypes).FirstOrDefaultAsync(p => p.ProductId == id);

				if (product == null)
				{
					return StatusCode(404, new CustomResponse(404, "Product not found", null));
				}

				var category = await _context.categories.FindAsync(updatedProduct.Category.CategoryId);
				if (category == null)
				{
					category = new Category
					{
						CategoryId = updatedProduct.Category.CategoryId,
						Name = updatedProduct.Category.Name,
						Description = updatedProduct.Category.Description
					};
					_context.categories.Add(category);
				}

				product.ProductName = updatedProduct.ProductName;
				product.AltName = updatedProduct.AltName;
				product.Dine_in_price = updatedProduct.Dine_in_price;
				product.Take_out_price = updatedProduct.Take_out_price;
				product.CategoryId = category.CategoryId;

				// Remove existing FoodTypes
				_context.foodTypes.RemoveRange(product.FoodTypes);

				// Add new FoodTypes
				product.FoodTypes = updatedProduct.FoodTypes.Select(ft => new FoodType
				{
					TypeId = ft.TypeId,
					TypeName = ft.TypeName,
					Description = ft.Description
				}).ToList();

				_context.Entry(product).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				var responseData = new
				{
					product.ProductId,
					product.ProductName,
					product.AltName,
					product.Dine_in_price,
					product.Take_out_price,
					FoodTypes = product.FoodTypes.Select(ft => new
					{
						ft.TypeId,
						ft.TypeName,
						ft.Description
					}),
					Category = new
					{
						product.Category.CategoryId,
						product.Category.Name,
						product.Category.Description
					}
				};

				return StatusCode(200, new CustomResponse(200, "Product updated successfully", responseData));
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteProduct(int id)
		{
			var product = await _context.products.FindAsync(id);
			if (product == null)
			{
				return StatusCode(404, new CustomResponse(404, "Product not found", null));
			}

			_context.products.Remove(product);
			await _context.SaveChangesAsync();

			return StatusCode(200, new CustomResponse(200, "Product deleted successfully", null));
		}

	}
}




