using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class BusinessHoursControllerTests
	{
		private readonly RiversideFishhutDbContext _context;

		public BusinessHoursControllerTests()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			_context.businessHours.Add(new BusinessHour { BusinessHourId = 1, DayOfWeek = "Monday", BusinessTime = "9am - 5pm" });
			_context.businessHours.Add(new BusinessHour { BusinessHourId = 2, DayOfWeek = "Tuesday", BusinessTime = "9am - 5pm" });
			_context.businessHours.Add(new BusinessHour { BusinessHourId = 3, DayOfWeek = "Wednesday", BusinessTime = "9am - 5pm" });
			_context.SaveChanges();
		}
		[Fact]
		public async Task TestGetBusinessHours()
		{
			// Arrange
			var controller = new BusinessHoursController(_context);

			// Act
			var result = await controller.GetBusinessHours().ConfigureAwait(false);

			// Assert
			Assert.NotNull(result);
			if (result.Result == null)
			{
				Assert.False(false, "GetBusinessHours() result is null");
			}
			else
			{
				Assert.IsType<OkObjectResult>(result.Result);
			}
		}

		// Add additional test methods for other controller methods here
	}
}




