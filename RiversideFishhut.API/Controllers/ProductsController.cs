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
						p.AltName,
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
		public async Task<ActionResult<CustomResponse>> PostProduct(ProductCreateRequest productCreateRequest)
		{
			try
			{
				Product product = new Product
				{
					ProductName = productCreateRequest.ProductName,
					AltName = productCreateRequest.AltName,
					Dine_in_price = productCreateRequest.Dine_in_price,
					Take_out_price = productCreateRequest.Take_out_price,
					CategoryId = productCreateRequest.Category.CategoryId,
				};

				foreach (var foodType in productCreateRequest.FoodTypes)
				{
					product.FoodTypes.Add(new FoodType
					{
						TypeId = foodType.TypeId,
						TypeName = foodType.TypeName,
						Description = foodType.Description,
					});
				}

				_context.products.Add(product);
				await _context.SaveChangesAsync();

				var responseData = new
				{
					product.ProductId,
					product.ProductName,
					product.AltName,
					product.Dine_in_price,
					product.Take_out_price,
					foodTypes = product.FoodTypes.Select(ft => new
					{
						ft.TypeId,
						ft.TypeName,
						ft.Description,
					}),
					Category = new
					{
						product.Category.CategoryId,
						product.Category.Name,
						product.Category.Description
					}
				};

				return CreatedAtAction(nameof(Getproducts), new { id = product.ProductId }, new CustomResponse(201, "Product created successfully", responseData));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// PUT: api/Products/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> UpdateProduct(int id, UpdateProductRequest updateProductRequest)
		{
			try
			{
				var product = await _context.products.Include(p => p.FoodTypes).FirstOrDefaultAsync(p => p.ProductId == id);

				if (product == null)
				{
					return NotFound(new CustomResponse(404, "Product not found", null));
				}

				product.ProductName = updateProductRequest.ProductName;
				product.AltName = updateProductRequest.AltName;
				product.Dine_in_price = updateProductRequest.Dine_in_price;
				product.Take_out_price = updateProductRequest.Take_out_price;
				product.CategoryId = updateProductRequest.Category.CategoryId;

				// Update food types
				product.FoodTypes.Clear();
				foreach (var foodType in updateProductRequest.FoodTypes)
				{
					product.FoodTypes.Add(new FoodType
					{
						TypeId = foodType.TypeId,
						TypeName = foodType.TypeName,
						Description = foodType.Description,
					});
				}

				await _context.SaveChangesAsync();

				return new CustomResponse(200, "Product updated successfully", null);
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
				return NotFound(new CustomResponse(404, "Product not found", null));
			}

			try
			{
				_context.products.Remove(product);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}

			return new CustomResponse(200, "Product deleted successfully", null);
		}

	}
}




