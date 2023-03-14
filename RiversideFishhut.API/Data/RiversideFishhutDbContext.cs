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
        public DbSet<OrderType> orderType { get; set; }
        public DbSet<OrderStatus> orderStatus { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderLineItem> orderLineItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                    BusinessHour = "Monday closed" + "Tuesday 11:30am - 7:30pm" + "Wednesday 11:30am - 7:30pm" + "Thursday 11:30am - 7:30pm" + "Friday 11:30am - 7:30pm" + "Saturday 11:30am - 8:00pm" + "Sunday closed" + "",
                }
                );

            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    StaffId = 1,
                    StaffName = "Staff 1",
                    Position = "Reception",
                    Password = "Password1",
                }, new Staff
                {
                    StaffId = 2,
                    StaffName = "Staff 2",
                    Position = "back kitchen",
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

            modelBuilder.Entity<FoodType>().HasData(
              new FoodType
              {
                  TypeId = 1,
                  TypeName = "2 PC Dinner",
                  Description = "This type is for 2 Pc fish with 1 pack chip.",
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
                }, new Product 
                {
                    ProductId = 2,
                    ProductName = "2Pc Cod & Chips",
                    Dine_in_price = 12,
                    Take_out_price = 11,
                    CategoryId = 1,
                }, new Product
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
            modelBuilder.Entity<Category>().HasData(
               new Category
               {
                   CategoryId= 1,
                   Description = "Main Dish",
                   TypeName= "2 PC Dinner",
                   foodTypeTypeId = 1,

               }
               );
            modelBuilder.Entity<OrderType>().HasData(
                new OrderType
                {
                    OrderTypeId = 1,
                    TypeName = "Dine In"
                },
                new OrderType
                {
                    OrderTypeId = 2,
                    TypeName = "Take Out"
                },
                new OrderType
                {
                    OrderTypeId = 3,
                    TypeName = "Phone Order"
                }
                );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus
                {
                    OrderStatusId = 1,
                    OrderStatusName = "Ordered"
                },
                new OrderStatus
                {
                    OrderStatusId = 2,
                    OrderStatusName = "In Progress"
                },
                new OrderStatus
                {
                    OrderStatusId = 3,
                    OrderStatusName = "Ready"
                },
                new OrderStatus
                {
                    OrderStatusId = 4,
                    OrderStatusName = "Complete"
                }
                );
        }

    }
}
