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
	public class FoodTypesController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public FoodTypesController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/FoodTypes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<object>>> GetFoodTypes()
		{
			// return only the required fields using anonymous type
			return await _context.foodTypes
				.Select(f => new { f.TypeId, f.TypeName, f.Description })
				.ToListAsync();
		}

		// POST: api/FoodTypes
		[HttpPost]
		public async Task<ActionResult<object>> PostFoodType(FoodType foodType)
		{
			// add the new food type to the database and save changes
			_context.foodTypes.Add(foodType);
			await _context.SaveChangesAsync();

			// retrieve the new food type and return only the required fields using anonymous type
			var result = await _context.foodTypes
				.Select(f => new { f.TypeId, f.TypeName, f.Description })
				.SingleAsync(f => f.TypeId == foodType.TypeId);

			return result;
		}

		// PUT: api/FoodTypes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFoodType(int id, FoodType foodType)
		{
			// retrieve the food type with the given id and update the TypeName and Description properties
			var existingFoodType = await _context.foodTypes.FindAsync(id);
			if (existingFoodType == null)
			{
				return NotFound();
			}

			existingFoodType.TypeName = foodType.TypeName;
			existingFoodType.Description = foodType.Description;

			_context.Entry(existingFoodType).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FoodTypeExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return Ok(new { status = "Success", message = $"FoodType with id {id} updated successfully" });
		}

		// DELETE: api/FoodTypes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFoodType(int id)
		{
			// retrieve the food type with the given id and remove it from the database
			var foodType = await _context.foodTypes.FindAsync(id);
			if (foodType == null)
			{
				return NotFound();
			}

			_context.foodTypes.Remove(foodType);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool FoodTypeExists(int id)
		{
			return _context.foodTypes.Any(e => e.TypeId == id);
		}
	}
}
