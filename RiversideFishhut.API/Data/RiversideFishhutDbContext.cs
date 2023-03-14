using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Data
{
	public class RiversideFishhutDbContext : DbContext
	{
		public RiversideFishhutDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Admin> admins { get; set; }
		public DbSet<Category> categories { get; set; }
		public DbSet<FoodType> foodTypes { get; set; }
		public DbSet<Product> products { get; set; }
		public DbSet<Staff> staffs { get; set; }
		public DbSet<WebsiteInfo> websiteInfos { get; set; }
		public DbSet<BusinessHour> businessHours { get; set; }
		public DbSet<CategoryFoodType> CategoryFoodTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CategoryFoodType>(builder =>
			{
				builder.HasKey(cf => new { cf.CategoryId, cf.FoodTypeId });
				builder.HasOne(cf => cf.Category)
					.WithMany(c => c.CategoryFoodTypes)
					.HasForeignKey(cf => cf.CategoryId);
				builder.HasOne(cf => cf.FoodType)
					.WithMany(ft => ft.CategoryFoodTypes)
					.HasForeignKey(cf => cf.FoodTypeId);
			});
			modelBuilder.Entity<Category>()
				.HasOne(c => c.foodType)
				.WithMany(ft => ft.Categories)
				.HasForeignKey(c => c.FoodTypeId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<WebsiteInfo>().HasData(
				new WebsiteInfo
				{
					InfoId = 1,
					StoreName = "Riverside Fishhut",
					Description = "This cozy restaurant specializes in traditional English fish and French fries, " +
								  "serving up freshly fried, crispy and flavorful fish sourced from local suppliers, " +
								  "and thick golden fries that make the perfect side dish.",
					Phone = "519-653-0788",
					OnlineOrderLink = "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites",
					Address = "157 king st west Cambridge, ON, N3H 1B5",
				}
			);

			modelBuilder.Entity<BusinessHour>().HasData(
				new BusinessHour
				{
					BusinessHourId = 1,
					dayOfWeek = "Monday",
					businessTime = "09:00 - 17:00",
				}
			);

			modelBuilder.Entity<Staff>().HasData(
				new Staff
				{
					StaffId = 1,
					roleId = "Admin",
					StaffName = "Staff 1",
					Description = "description1",
					Password = "Password1",
				},
				new Staff
				{
					StaffId = 2,
					roleId = "Staff",
					StaffName = "Staff 2",
					Description = "description2",
					Password = "Password2",
				}
			);

			modelBuilder.Entity<Admin>().HasData(
				new Admin
				{
					AdminId = 1,
					AdminName = "Admin",
					AdminAddress = "Admin@gmail.com",
					AdminPassword = "Admin123",
				}
			);

			modelBuilder.Entity<Category>().HasData(
				new Category
				{
					CategoryId = 1,
					Description = "Main Dish",
					TypeName = "2 PC Dinner",
					FoodTypeId = 1,
				}
				);

			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					ProductName = "2Pc Whitefish & Chips",
					Dine_in_price = 10,
					Take_out_price = 9,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 2,
					ProductName = "2Pc Cod & Chips",
					Dine_in_price = 12,
					Take_out_price = 11,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 3,
					ProductName = "2Pc Haddock & Chips",
					Dine_in_price = 14,
					Take_out_price = 13,
					CategoryId = 1,
				}, new Product
				{
					ProductId = 4,
					ProductName = "2Pc Halibut & Chips",
					Dine_in_price = 16,
					Take_out_price = 15,
					CategoryId = 1,
				}
				);


			modelBuilder.Entity<FoodType>().HasData(
				new FoodType
				{
					TypeId = 1,
					TypeName = "2 PC Dinner",
					Description = "This type is for 2 Pc fish with 1 pack chip.",
				},
				new FoodType
				{
					TypeId = 2,
					TypeName = "FoodType2",
					Description = "Description for FoodType2",
				},
				new FoodType
				{
					TypeId = 3,
					TypeName = "FoodType3",
					Description = "Description for FoodType3",
				},
				new FoodType
				{
					TypeId = 4,
					TypeName = "FoodType4",
					Description = "Description for FoodType4",
				});

			modelBuilder.Entity<CategoryFoodType>().HasData(
				new CategoryFoodType 
				{
					CategoryId = 1, 
					FoodTypeId = 1 
				});
		}
	}
}



