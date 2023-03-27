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
					.Include(p => p.ProductFoodTypes)
						.ThenInclude(pft => pft.FoodType)
					.Select(p => new
					{
						p.ProductId,
						p.ProductName,
						p.AltName,
						p.Dine_in_price,
						p.Take_out_price,
						FoodTypes = p.ProductFoodTypes.Select(pft => pft.FoodType.TypeName).ToList(),
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
					FoodTypes = new List<FoodType>()
				};

				if (productCreateRequest.CategoryId.HasValue)
				{
					var category = await _context.categories.FindAsync(productCreateRequest.CategoryId.Value);
					if (category == null)
					{
						return NotFound(new CustomResponse(404, "Category not found", null));
					}

					product.CategoryId = productCreateRequest.CategoryId.Value;
				}

				if (productCreateRequest.FoodTypes != null && productCreateRequest.FoodTypes.Any())
				{
					foreach (var foodType in productCreateRequest.FoodTypes)
					{
						var existingFoodType = await _context.foodTypes.FindAsync(foodType.TypeId);
						if (existingFoodType == null)
						{
							return NotFound("Food type not found");
						}

						product.FoodTypes.Add(existingFoodType);
					}
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
					FoodTypes = product.FoodTypes.Select(ft => new
					{
						ft.TypeId,
						ft.TypeName,
						ft.Description,
					}),
					Category = product.Category != null ? new
					{
						product.Category.CategoryId,
						product.Category.Name,
						product.Category.Description
					} : null
				};

				return CreatedAtAction(nameof(Getproducts), new { id = product.ProductId }, new CustomResponse(201, "Product created successfully", responseData));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

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
				product.CategoryId = updateProductRequest.CategoryId;

				// Update food types
				product.FoodTypes.Clear();
				if (updateProductRequest.FoodTypeIds != null && updateProductRequest.FoodTypeIds.Any())
				{
					foreach (var typeId in updateProductRequest.FoodTypeIds)
					{
						var existingFoodType = await _context.foodTypes.FindAsync(typeId);
						if (existingFoodType == null)
						{
							return NotFound("Food type not found");
						}

						product.FoodTypes.Add(existingFoodType);
					}
				}

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
						ft.Description,
					}),
					Category = product.Category != null ? new
					{
						product.Category.CategoryId,
						product.Category.Name,
						product.Category.Description
					} : null
				};

				return new CustomResponse(200, "Product updated successfully", responseData);
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




