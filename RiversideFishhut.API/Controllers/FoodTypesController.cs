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

		private bool FoodTypeExists(int id)
		{
			return _context.foodTypes.Any(e => e.TypeId == id);
		}

		// GET: api/FoodTypes
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetFoodTypes()
		{
			try
			{
				var foodTypes = await _context.foodTypes
					.Select(f => new
					{
						f.TypeId,
						f.TypeName,
						f.Description
					})
					.ToListAsync();

				return new CustomResponse(200, "Food types retrieved successfully", foodTypes);
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		// POST: api/FoodTypes
		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostFoodType(FoodType foodType)
		{
			try
			{
				// Add the new food type to the database and save changes
				_context.foodTypes.Add(foodType);
				await _context.SaveChangesAsync();

				// Retrieve the new food type and return only the required fields using anonymous type
				var result = await _context.foodTypes
					.Select(f => new
					{
						f.TypeId,
						f.TypeName,
						f.Description
					})
					.SingleAsync(f => f.TypeId == foodType.TypeId);

				return new CustomResponse(200, "Food type created successfully", new List<object> { result });
			}
			catch (Exception ex)
			{
				
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
		// PUT: api/FoodTypes/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> PutFoodType(int id, FoodType foodType)
		{
			// retrieve the food type with the given id and update the TypeName and Description properties
			var existingFoodType = await _context.foodTypes.FindAsync(id);
			if (existingFoodType == null)
			{
				return StatusCode(404, new CustomResponse(404, "Food Type not found", null));
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
					return StatusCode(404, new CustomResponse(404, "Food Type not found", null));
				}
				else
				{
					throw;
				}
			}

			return StatusCode(200, new CustomResponse(200, "Food Type updated successfully", null));
		}

		// DELETE: api/FoodTypes/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteFoodType(int id)
		{
			// retrieve the food type with the given id and remove it from the database
			var foodType = await _context.foodTypes.FindAsync(id);
			if (foodType == null)
			{
				return StatusCode(404, new CustomResponse(404, "Food Type not found", null));
			}

			_context.foodTypes.Remove(foodType);
			await _context.SaveChangesAsync();

			return StatusCode(200, new CustomResponse(200, "Food Type deleted successfully", null));
		}

		
	}
}
