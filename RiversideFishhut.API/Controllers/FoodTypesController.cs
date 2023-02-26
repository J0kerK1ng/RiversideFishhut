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
        public async Task<ActionResult<IEnumerable<FoodType>>> GetfoodTypes()
        {
            var foodtype = await _context.foodTypes.ToListAsync();
            return Ok(foodtype);
        }

        // GET: api/FoodTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodType>> GetFoodType(int id)
        {
            var foodType = await _context.foodTypes.FindAsync(id);

            if (foodType == null)
            {
                return NotFound();
            }

            return foodType;
        }

        // PUT: api/FoodTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodType(int id, FoodType foodType)
        {
            if (id != foodType.TypeId)
            {
                return BadRequest();
            }

            _context.Entry(foodType).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/FoodTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodType>> PostFoodType(FoodType foodType)
        {
            _context.foodTypes.Add(foodType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodType", new { id = foodType.TypeId }, foodType);
        }

        // DELETE: api/FoodTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodType(int id)
        {
            var foodType = await _context.foodTypes.FindAsync(id);
            if (foodType == null)
            {
                return NotFound("Invalid Id");
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
