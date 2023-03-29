using Microsoft.EntityFrameworkCore;
using RiversideFishhut.API.Data;

namespace RiversideFishhut.API.Data
{
	public class RiversideFishhutDbContext : DbContext
	{

		public RiversideFishhutDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Category> categories { get; set; }
		public DbSet<FoodType> foodTypes { get; set; }
		public DbSet<Product> products { get; set; }
		public DbSet<Staff> staffs { get; set; }
		public DbSet<WebsiteInfo> websiteInfos { get; set; }
		public DbSet<BusinessHour> businessHours { get; set; }
		public DbSet<Role> roles { get; set; }
		public DbSet<ProductFoodType> productFoodTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Staff>()
				.HasOne(s => s.Role)
				.WithMany()
				.HasForeignKey(s => s.RoleId);

			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductFoodTypes)
				.WithOne(pf => pf.Product)
				.HasForeignKey(pf => pf.ProductId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<FoodType>()
				.HasMany(ft => ft.ProductFoodTypes)
				.WithOne(pft => pft.FoodType)
				.HasForeignKey(pft => pft.TypeId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProductFoodType>()
				.HasKey(pf => new { pf.ProductId, pf.TypeId });

			modelBuilder.Entity<ProductFoodType>()
				.HasOne(pf => pf.Product)
				.WithMany(p => p.ProductFoodTypes)
				.HasForeignKey(pf => pf.ProductId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProductFoodType>()
				.HasOne(pft => pft.FoodType)
				.WithMany(ft => ft.ProductFoodTypes)
				.HasForeignKey(pft => pft.TypeId)
				.OnDelete(DeleteBehavior.NoAction);



			// Add seed data for Roles
			modelBuilder.Entity<Role>().HasData(
				new Role {
					RoleId = 1, 
					RoleName = "Admin", 
					RoleDescription = "Administrator" 
				},
				new Role {
					RoleId = 2, 
					RoleName = "Staff", 
					RoleDescription = "Staff member" 
				},
				new Role {
					RoleId = 3, 
					RoleName = "Waiter", 
					RoleDescription = "Waiter" 
				}
			);


			// Add seed data for Staffs
			modelBuilder.Entity<Staff>().HasData(
				new Staff
				{
					StaffId = 1,
					StaffName = "Staff 1",
					Password = "Password1",
					Email = "asdsad@gmail.com",
					RoleId = 1
				},
				new Staff
				{
					StaffId = 2,
					StaffName = "Staff 2",
					Password = "Password2",
					Email = "sad@gmail.com",
					RoleId = 2
				}
			);

			// Add seed data for WebsiteInfo
			modelBuilder.Entity<WebsiteInfo>().HasData(
				new WebsiteInfo
				{
					InfoId = 1,
					StoreName = "Riverside Fishhut",
					LogoImage = "Logo name",
					Description = "This cozy restaurant specializes in traditional English fish and French fries, " +
								  "serving up freshly fried, crispy and flavorful fish sourced from local suppliers, " +
								  "and thick golden fries that make the perfect side dish.",
					PhoneNumber = "519-653-0788",
					OnlineOrderLink = "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites",
					Address = "157 king st west Cambridge, ON, N3H 1B5",
				}
			);

			// Add seed data for BusinessHours
			modelBuilder.Entity<BusinessHour>().HasData(
				new BusinessHour
				{
					BusinessHourId = 1,
					DayOfWeek = "Monday",
					BusinessTime = "Closed",
				},
				new BusinessHour
				{
					BusinessHourId = 2,
					DayOfWeek = "Tuesday",
					BusinessTime = "09:00 - 17:00",
				},
				new BusinessHour
				{
					BusinessHourId = 3,
					DayOfWeek = "Wednesday",
					BusinessTime = "09:00 - 17:00",
				},
				new BusinessHour
				{
					BusinessHourId = 4,
					DayOfWeek = "Thursday",
					BusinessTime = "09:00 - 17:00",
				},
				new BusinessHour
				{
					BusinessHourId = 5,
					DayOfWeek = "Friday",
					BusinessTime = "09:00 - 17:00",
				},
				new BusinessHour
				{
					BusinessHourId = 6,
					DayOfWeek = "Saturday",
					BusinessTime = "09:00 - 17:00",
				}, new BusinessHour
				{
					BusinessHourId = 7,
					DayOfWeek = "Sunday",
					BusinessTime = "Closed",
				}

			);

			// Add seed data for FoodTypes
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
					TypeName = "Fish ",
					Description = "This is fish.",
				},
				new FoodType
				{
					TypeId = 3,
					TypeName = "Deal",
					Description = "This product has discount right now.",
				}
			);

			// Add seed data for Categories
			modelBuilder.Entity<Category>().HasData(
				new Category
				{
					CategoryId = 1,
					Description = "Main Dish",
					Name = "2 PC Dinner",
				}
			);

			// Add seed data for Products
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					ProductName = "2Pc Whitefish & Chips",
					AltName = "2 PC W/C",
					Description = "Description",
					Dine_in_price = 10,
					Take_out_price = 9,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 2,
					ProductName = "2Pc Cod & Chips",
					AltName = "2 PC COD/C",
					Description = "Description",
					Dine_in_price = 12,
					Take_out_price = 11,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 3,
					ProductName = "2Pc Haddock & Chips",
					AltName = "2 PC HDK/C",
					Description = "Description",
					Dine_in_price = 14,
					Take_out_price = 13,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 4,
					ProductName = "2Pc Halibut & Chips",
					AltName = "2PC HB/C",
					Description = "Description",
					Dine_in_price = 16,
					Take_out_price = 15,
					CategoryId = 1,
				}
			);

			modelBuilder.Entity<ProductFoodType>().HasData(
				new ProductFoodType
				{
					ProductId = 1,
					TypeId = 1
				},
				new ProductFoodType
				{
					ProductId = 1,
					TypeId = 2
				},
				new ProductFoodType
				{
					ProductId = 1,
					TypeId = 3
				},
				new ProductFoodType
				{
					ProductId = 2,
					TypeId = 1
				},
				new ProductFoodType
				{
					ProductId = 2,
					TypeId = 2
				},
				new ProductFoodType
				{
					ProductId = 2,
					TypeId = 3
				},
				new ProductFoodType
				{
					ProductId = 3,
					TypeId = 1
				},
				new ProductFoodType
				{
					ProductId = 3,
					TypeId = 2
				},
				new ProductFoodType
				{
					ProductId = 3,
					TypeId = 3
				},
				new ProductFoodType
				{
					ProductId = 4,
					TypeId = 1
				},
				new ProductFoodType
				{
					ProductId = 4,
					TypeId = 2
				},
				new ProductFoodType
				{
					ProductId = 4,
					TypeId = 3
				}

			);

		}
	} 
}


