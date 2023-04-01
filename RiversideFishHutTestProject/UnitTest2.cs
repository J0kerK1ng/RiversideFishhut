
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class UnitTest2
	{
		private readonly RiversideFishhutDbContext _context;

		public UnitTest2()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb2")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			// Add seed data for products here
		}

		[Fact]
		public async Task TestGetProduct()
		{
			// Arrange
			var controller = new ProductsController(_context);

			// Act
			var result = await controller.Getproducts().ConfigureAwait(false);

			// Assert
			Assert.NotNull(result);
			if (result.Result == null)
			{
				Assert.False(false, "GetProduct() result is null");
			}
			else
			{
				Assert.IsType<OkObjectResult>(result.Result);
			}
		}
	}
}

