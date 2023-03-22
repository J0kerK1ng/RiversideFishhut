using Microsoft.EntityFrameworkCore;
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
		public DbSet<Role> roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Admin>()
				.HasOne(a => a.Role)
				.WithMany()
				.HasForeignKey(a => a.RoleId);

			modelBuilder.Entity<Staff>()
				.HasOne(s => s.Role)
				.WithMany()
				.HasForeignKey(s => s.RoleId);


			modelBuilder.Entity<Product>()
				.HasMany(p => p.FoodTypes)
				.WithOne(ft => ft.Product)
				.HasForeignKey(ft => ft.ProductId);

			// Add seed data for Roles
			modelBuilder.Entity<Role>().HasData(
				new Role { RoleId = 1, RoleName = "Admin", RoleDescription = "Administrator" },
				new Role { RoleId = 2, RoleName = "Staff", RoleDescription = "Staff member" }
			);

			// Add seed data for Admins
			modelBuilder.Entity<Admin>().HasData(
				new Admin
				{
					AdminId = 1,
					AdminName = "Admin",
					AdminEmailAddress = "Admin@gmail.com",
					AdminPassword = "Admin123",
					RoleId = 1
				}
			);

			// Add seed data for Staffs
			modelBuilder.Entity<Staff>().HasData(
				new Staff
				{
					StaffId = 1,
					StaffName = "Staff 1",
					Description = "description1",
					Password = "Password1",
					RoleId = 1
				},
				new Staff
				{
					StaffId = 2,
					StaffName = "Staff 2",
					Description = "description2",
					Password = "Password2",
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
					BusinessTime = "09:00 - 17:00",
				}
			);

			// Add seed data for FoodTypes
			modelBuilder.Entity<FoodType>().HasData(
				new FoodType
				{
					TypeId = 1,
					TypeName = "2 PC Dinner",
					Description = "This type is for 2 Pc fish with 1 pack chip.",
					ProductId = 1,
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
					Dine_in_price = 10,
					Take_out_price = 9,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 2,
					ProductName = "2Pc Cod & Chips",
					AltName = "2 PC COD/C",
					Dine_in_price = 12,
					Take_out_price = 11,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 3,
					ProductName = "2Pc Haddock & Chips",
					AltName = "2 PC HDK/C",
					Dine_in_price = 14,
					Take_out_price = 13,
					CategoryId = 1,
				},
				new Product
				{
					ProductId = 4,
					ProductName = "2Pc Halibut & Chips",
					AltName = "2PC HB/C",
					Dine_in_price = 16,
					Take_out_price = 15,
					CategoryId = 1,
				}
			);
		}
	} 
}


