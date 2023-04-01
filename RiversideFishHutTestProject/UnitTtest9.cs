
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class UnitTest9
	{
		private readonly RiversideFishhutDbContext _context;

		public UnitTest9()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb9")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			// Add seed data for the Staff entity here
		}

		[Fact]
		public async Task TestCreateRole()
		{
			// Arrange
			var controller = new RolesController(_context);
			var roleCreateRequest = new RoleCreateRequest
			{
				RoleName = "Test Role",
				Description = "Test role description"
			};

			// Act
			var result = await controller.CreateRole(roleCreateRequest);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(201, ((ObjectResult)result.Result).StatusCode);
		}



	}
}


