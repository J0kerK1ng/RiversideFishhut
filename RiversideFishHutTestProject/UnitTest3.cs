
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Controllers;
using RiversideFishhut.API.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace RiversideFishhut.API.Tests
{
	public class UnitTest3
	{
		private readonly RiversideFishhutDbContext _context;

		public UnitTest3()
		{
			var options = new DbContextOptionsBuilder<RiversideFishhutDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb3")
				.Options;

			_context = new RiversideFishhutDbContext(options);

			// Seed data for testing
			// Add seed data for the Staff entity here
		}

		[Fact]
		public async Task TestDeleteStaff()
		{
			// Arrange
			var controller = new StaffsController(_context);

			// Act
			var result = await controller.DeleteStaffMember(1).ConfigureAwait(false);

			// Assert
			Assert.NotNull(result);
			if (result.Result != null)
			{
				Assert.False(false, "DeleteStaff() result is null");
			}
			else
			{
				Assert.IsType<OkObjectResult>(result.Result);
			}
		}
	}
}

