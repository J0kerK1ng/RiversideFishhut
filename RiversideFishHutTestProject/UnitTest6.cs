
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class UnitTest6
	{
		private readonly RiversideFishhutDbContext _context;

		public UnitTest6()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb6")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			// Add seed data for the Staff entity here
		}

		[Fact]
		public async Task TestGetRoles()
		{
			// Arrange
			var controller = new RolesController(_context);

			// Act
			var result = await controller.GetRoles();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result.Value.Status);
		}


	}
}
