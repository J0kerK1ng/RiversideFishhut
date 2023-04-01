using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
	{
		private readonly RiversideFishhutDbContext _context;

		public OrdersController(RiversideFishhutDbContext context)
		{
			_context = context;
		}

		// GET: api/Orders
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
		{
			return await _context.order.ToListAsync();
		}


		// GET: api/Orders
		[HttpGet("Recent5")]
		public async Task<ActionResult<IEnumerable<Order>>> GetRecent5Orders()
		{
			return await _context.order.OrderByDescending(o => o.OrderId).Take(5).ToListAsync();
		}


		[HttpGet("TodaysOrders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetTodaysOrders()
		{
			DateTime today = DateTime.Today;
			DateTime tomorrow = today.AddDays(1);

			return await _context.order.Where(o => o.OrderDate >= today && o.OrderDate < tomorrow).OrderByDescending(o => o.OrderId).ToListAsync();
		}

		[HttpGet("PendingOrders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetPendingOrders()
		{
			return await _context.order.Where(s => s.OrderStatusId != 4).ToListAsync();
		}

		[HttpGet("TodaysCompleteOrders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetTodaysCompleteOrders()
		{
			DateTime today = DateTime.Today;
			DateTime tomorrow = today.AddDays(1);

			return await _context.order.Where(s => s.OrderStatusId == 4 && (s.OrderDate >= today && s.OrderDate < tomorrow)).ToListAsync();
		}

		[HttpGet("TodaysPaidOrders")]
		public async Task<ActionResult<IEnumerable<Order>>> GetTodaysPaidOrders()
		{
			DateTime today = DateTime.Today;
			DateTime tomorrow = today.AddDays(1);

			return await _context.order.Where(s => s.PaymentStatus == true && (s.OrderDate >= today && s.OrderDate < tomorrow)).ToListAsync();
		}

		[HttpGet("TodaysRevenue")]
		public async Task<ActionResult<decimal>> GetTodaysRevenue()
		{
			DateTime today = DateTime.Today;
			DateTime tomorrow = today.AddDays(1);

			List<Order> todaysOrders = await _context.order.Where(s => s.PaymentStatus == true && (s.OrderDate >= today && s.OrderDate < tomorrow)).ToListAsync();

			decimal total = 0;

			foreach (var order in todaysOrders)
			{
				total += order.TotalCost;
			}

			return total;
		}

		[HttpGet("Tables")]
		public async Task<ActionResult<IEnumerable<string[]>>> GetTables()
		{
			List<Order> currentOrders = await _context.order.Where(s => s.PaymentStatus == false && (s.table != null || s.table != "")).ToListAsync();
			List<string[]> tables = new List<string[]>();

			for (int i = 1; i < 13; i++)
			{
				bool added = false;

				foreach (var order in currentOrders)
				{
					if (order.table == Convert.ToString(i))
					{
						string[] newTable = new string[] { Convert.ToString(i), "used" };
						tables.Add(newTable);
						added = true;
					}
				}

				if (added == false)
				{
					string[] newTable = new string[] { Convert.ToString(i), "available" };
					tables.Add(newTable);
				}
			}

			return tables;
		}


		[HttpGet("RevenueHistory")]
		public async Task<ActionResult<IEnumerable<string[]>>> GetRevenueHistory()
		{
			List<string[]> revenueByDay = new List<string[]>();

			// Get all orders that have been paid
			var paidOrders = await _context.order.Where(o => o.PaymentStatus).ToListAsync();

			// Group orders by date
			var ordersByDate = paidOrders.GroupBy(o => o.OrderDate.Date);

			// Calculate revenue for each date
			foreach (var orderGroup in ordersByDate)
			{
				DateTime date = orderGroup.Key;
				decimal dineInTotal = 0;
				decimal takeOutTotal = 0;
				decimal phoneTotal = 0;
				decimal total = 0;

				// Calculate revenue for each order type
				foreach (var order in orderGroup)
				{
					if (order.OrderTypeId == 1)
					{
						dineInTotal += order.TotalCost;
					}
					else if (order.OrderTypeId == 2)
					{
						takeOutTotal += order.TotalCost;
					}
					else
					{
						phoneTotal += order.TotalCost;
					}

					total += order.TotalCost;
				}

				string[] newRevenue = new string[] { date.ToString(), dineInTotal.ToString(), takeOutTotal.ToString(), phoneTotal.ToString(), total.ToString() };

				revenueByDay.Add(newRevenue);
			}

			return revenueByDay;
		}


		// GET: api/Orders/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(int id)
		{
			var order = await _context.order.FindAsync(id);

			if (order == null)
			{
				return NotFound();
			}

			return order;
		}

		// PUT: api/Orders/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrder(int id, Order order)
		{
			if (id != order.OrderId)
			{
				return BadRequest();
			}

			_context.Entry(order).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
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

		// POST: api/Orders
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			_context.order.Add(order);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
		}

		// DELETE: api/Orders/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var order = await _context.order.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}

			_context.order.Remove(order);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool OrderExists(int id)
		{
			return _context.order.Any(e => e.OrderId == id);
		}
	}
}
