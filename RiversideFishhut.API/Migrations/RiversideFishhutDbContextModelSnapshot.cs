﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiversideFishhut.API.Data;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    [DbContext(typeof(RiversideFishhutDbContext))]
    partial class RiversideFishhutDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RiversideFishhut.API.Data.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("admins");

                    b.HasData(
                        new
                        {
                            AdminId = 1,
                            AdminAddress = "Admin@gmail.com",
                            AdminName = "Admin",
                            AdminPassword = "Admin123"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("foodTypeTypeId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("foodTypeTypeId");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Main Dish",
                            TypeName = "2 PC Dinner",
                            foodTypeTypeId = 1
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.FoodType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeId");

                    b.ToTable("foodTypes");

                    b.HasData(
                        new
                        {
                            TypeId = 1,
                            Description = "This type is for 2 Pc fish with 1 pack chip.",
                            TypeName = "2 PC Dinner"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStatusId"), 1L, 1);

                    b.Property<string>("OrderStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderStatusId");

                    b.ToTable("orderStatus");

                    b.HasData(
                        new
                        {
                            OrderStatusId = 1,
                            OrderStatusName = "Ordered"
                        },
                        new
                        {
                            OrderStatusId = 2,
                            OrderStatusName = "In Progress"
                        },
                        new
                        {
                            OrderStatusId = 3,
                            OrderStatusName = "Ready"
                        },
                        new
                        {
                            OrderStatusId = 4,
                            OrderStatusName = "Complete"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.OrderType", b =>
                {
                    b.Property<int>("OrderTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderTypeId"), 1L, 1);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderTypeId");

                    b.ToTable("orderType");

                    b.HasData(
                        new
                        {
                            OrderTypeId = 1,
                            TypeName = "Dine In"
                        },
                        new
                        {
                            OrderTypeId = 2,
                            TypeName = "Take Out"
                        },
                        new
                        {
                            OrderTypeId = 3,
                            TypeName = "Phone Order"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<double>("Dine_in_price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Take_out_price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Dine_in_price = 10.0,
                            ProductName = "2Pc Whitefish & Chips",
                            Take_out_price = 9.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Dine_in_price = 12.0,
                            ProductName = "2Pc Cod & Chips",
                            Take_out_price = 11.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1,
                            Dine_in_price = 14.0,
                            ProductName = "2Pc Haddock & Chips",
                            Take_out_price = 13.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1,
                            Dine_in_price = 16.0,
                            ProductName = "2Pc Halibut & Chips",
                            Take_out_price = 15.0
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffId");

                    b.ToTable("staffs");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            Password = "Password1",
                            Position = "Reception",
                            StaffName = "Staff 1"
                        },
                        new
                        {
                            StaffId = 2,
                            Password = "Password2",
                            Position = "back kitchen",
                            StaffName = "Staff 2"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.WebsiteInfo", b =>
                {
                    b.Property<int>("InfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfoId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineOrderLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InfoId");

                    b.ToTable("websiteInfos");

                    b.HasData(
                        new
                        {
                            InfoId = 1,
                            Address = "157 king st west Cambridge, ON, N3H 1B5",
                            BusinessHour = "Monday closedTuesday 11:30am - 7:30pmWednesday 11:30am - 7:30pmThursday 11:30am - 7:30pmFriday 11:30am - 7:30pmSaturday 11:30am - 8:00pmSunday closed",
                            Description = "This cozy restaurant specializes in traditional English fish and French fries, serving up freshly fried, crispy and flavorful fish sourced from local suppliers, and thick golden fries that make the perfect side dish.",
                            OnlineOrderLink = "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites",
                            Phone = "519-653-0788",
                            StoreName = "Riverside Fishhut"
                        });
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Category", b =>
                {
                    b.HasOne("RiversideFishhut.API.Data.FoodType", "foodType")
                        .WithMany("Categories")
                        .HasForeignKey("foodTypeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("foodType");
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Product", b =>
                {
                    b.HasOne("RiversideFishhut.API.Data.Category", "category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("RiversideFishhut.API.Data.FoodType", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
