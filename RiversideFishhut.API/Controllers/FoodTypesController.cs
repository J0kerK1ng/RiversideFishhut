using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/product-foodType")]
	[ApiController]
	[Authorize]
	public class FoodTypesController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public FoodTypesController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/FoodTypes
		[HttpGet]
		public async Task<ActionResult<CustomResponse>> GetFoodTypes()
		{
			try
			{
				var foodTypes = await _context.foodTypes.Select(ft => new
				{
					ft.TypeId,
					ft.TypeName,
					ft.Description
				}).ToListAsync();

				return new CustomResponse(200, "Food types retrieved successfully", foodTypes);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}

		[HttpPost]
		public async Task<ActionResult<CustomResponse>> PostFoodType(CreateFoodTypeRequest createFoodTypeRequest)
		{
			try
			{
				FoodType foodType = new FoodType
				{
					TypeName = createFoodTypeRequest.TypeName,
					Description = createFoodTypeRequest.Description
				};

				_context.foodTypes.Add(foodType);
				await _context.SaveChangesAsync();

				var responseData = new
				{
					foodType.TypeId,
					foodType.TypeName,
					foodType.Description
				};

				return CreatedAtAction(nameof(GetFoodTypes), new { id = foodType.TypeId }, new CustomResponse(201, "Food type created successfully", responseData));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
				//return StatusCode(500, new CustomResponse(500, $"Internal Server Error: {ex.Message}. Inner exception: {ex.InnerException?.Message}", null));

			}
		}




		// PUT: api/FoodTypes/5
		[HttpPut("{id}")]
		public async Task<ActionResult<CustomResponse>> UpdateFoodType(int id, UpdateFoodTypeRequest updateFoodTypeRequest)
		{
			try
			{
				var foodType = await _context.foodTypes.FindAsync(id);

				if (foodType == null)
				{
					return NotFound(new CustomResponse(404, "Food type not found", null));
				}

				foodType.TypeName = updateFoodTypeRequest.TypeName;
				foodType.Description = updateFoodTypeRequest.Description;

				await _context.SaveChangesAsync();

				var responseData = new
				{
					foodType.TypeId,
					foodType.TypeName,
					foodType.Description
				};

				return new CustomResponse(200, "Food type updated successfully", responseData);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}


		// DELETE: api/FoodTypes/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomResponse>> DeleteFoodType(int id)
		{
			var foodType = await _context.foodTypes.FindAsync(id);
			if (foodType == null)
			{
				return NotFound(new CustomResponse(404, "Food type not found", null));
			}

			try
			{
				_context.foodTypes.Remove(foodType);
				await _context.SaveChangesAsync();

				return new CustomResponse(200, "Food type deleted successfully", null);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new CustomResponse(500, "Internal Server Error", null));
			}
		}
	}
}
