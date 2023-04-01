
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class UnitTest5
	{
		private readonly RiversideFishhutDbContext _context;

		public UnitTest5()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb5")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			// Add seed data for the Product entity here
		}

		[Fact]
		public async Task TestDeleteProduct()
		{
			// Arrange
			var controller = new ProductsController(_context);

			// Act
			var result = await controller.DeleteProduct(1).ConfigureAwait(false);

			// Assert
			Assert.NotNull(result);
			if (result.Result != null)
			{
				Assert.False(false, "DeleteProduct() result is null");
			}
			else
			{
				Assert.IsType<OkObjectResult>(result.Result);
			}
		}
	}
}

