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
	public class ProductsController : ControllerBase
	{
		private bool ProductExists(int id)
		{
			return _context.products.Any(e => e.ProductId == id);
		}
		private readonly RiversideFishhutDbContext _context;

		public ProductsController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<object>>> Getproducts()
		{
			var products = await _context.products
				.Include(p => p.category)
				.Include(p => p.category.foodType)
				.Select(p => new
				{
					p.ProductId,
					p.ProductName,
					p.Dine_in_price,
					p.Take_out_price,
					TypeName = p.category.foodType.TypeName
				})
				.ToListAsync();

			return Ok(products);
		}

		// GET: api/Products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<object>> GetProduct(int id)
		{
			var product = await _context.products
				.Include(p => p.category)
				.ThenInclude(c => c.foodType) // Add this line to include the FoodType through the Category
				.Where(p => p.ProductId == id)
				.Select(p => new
				{
					p.ProductId,
					p.ProductName,
					p.Dine_in_price,
					p.Take_out_price,
					TypeId = p.category.foodType.TypeId, // Change this line to access FoodType through the Category
					TypeName = p.category.foodType.TypeName, // Change this line to access FoodType through the Category
					p.category.CategoryId,
					p.category.Description
				})
				.FirstOrDefaultAsync();

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		// PUT: api/Products/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.ProductId)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
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

		// POST: api/Products
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			var category = await _context.categories
				.FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);

			if (category == null)
			{
				return BadRequest("Invalid Category Id");
			}

			var foodType = await _context.foodTypes
				.FirstOrDefaultAsync(f => f.TypeId == category.FoodTypeId);

			if (foodType == null)
			{
				return BadRequest("Invalid FoodType Id");
			}

			product.category = category;

			_context.products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
		}




		// DELETE: api/Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.products.FindAsync(id);
			if (product == null)
			{
				return NotFound("Invalid Id");
			}

			// Remove product from category.Products
			var category = await _context.categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.CategoryId == product.CategoryId);
			category.Products.Remove(product);

			_context.products.Remove(product);
			_context.SaveChanges();

			return NoContent();
		}
	}
}

		