﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class Migrationwithdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "businessHours",
                columns: table => new
                {
                    BusinessHourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessHours", x => x.BusinessHourId);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "orderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "orderType",
                columns: table => new
                {
                    OrderTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderType", x => x.OrderTypeId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "websiteInfos",
                columns: table => new
                {
                    InfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlineOrderLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_websiteInfos", x => x.InfoId);
                });

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_staffs_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderTypeId = table.Column<int>(type: "int", nullable: false),
                    table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    BeforeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_order_orderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "orderType",
                        principalColumn: "OrderTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dine_in_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Take_out_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_products_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "foodTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodTypes", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_foodTypes_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "productFoodTypes",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productFoodTypes", x => new { x.ProductId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_productFoodTypes_foodTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "foodTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_productFoodTypes_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.InsertData(
                table: "businessHours",
                columns: new[] { "BusinessHourId", "BusinessTime", "DayOfWeek" },
                values: new object[,]
                {
                    { 1, "Closed", "Monday" },
                    { 2, "09:00 - 17:00", "Tuesday" },
                    { 3, "09:00 - 17:00", "Wednesday" },
                    { 4, "09:00 - 17:00", "Thursday" },
                    { 5, "09:00 - 17:00", "Friday" },
                    { 6, "09:00 - 17:00", "Saturday" },
                    { 7, "Closed", "Sunday" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 1, "Main Dish", "2 PC Dinner" });

            migrationBuilder.InsertData(
                table: "foodTypes",
                columns: new[] { "TypeId", "Description", "ProductId", "TypeName" },
                values: new object[,]
                {
                    { 1, "This type is for 2 Pc fish with 1 pack chip.", null, "2 PC Dinner" },
                    { 2, "This is fish.", null, "Fish " },
                    { 3, "This product has discount right now.", null, "Deal" }
                });

            migrationBuilder.InsertData(
                table: "orderStatus",
                columns: new[] { "OrderStatusId", "OrderStatusName" },
                values: new object[,]
                {
                    { 1, "In Progress" },
                    { 2, "Ready" },
                    { 3, "Complete" }
                });

            migrationBuilder.InsertData(
                table: "orderType",
                columns: new[] { "OrderTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Dine In" },
                    { 2, "Take Out" },
                    { 3, "Phone Order" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "RoleId", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Staff member", "Staff" },
                    { 3, "Waiter", "Waiter" }
                });

            migrationBuilder.InsertData(
                table: "websiteInfos",
                columns: new[] { "InfoId", "Address", "Description", "LogoImage", "OnlineOrderLink", "PhoneNumber", "StoreName" },
                values: new object[] { 1, "157 king st west Cambridge, ON, N3H 1B5", "This cozy restaurant specializes in traditional English fish and French fries, serving up freshly fried, crispy and flavorful fish sourced from local suppliers, and thick golden fries that make the perfect side dish.", "Logo name", "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites", "519-653-0788", "Riverside Fishhut" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "AltName", "CategoryId", "Description", "Dine_in_price", "OrderId", "ProductName", "Take_out_price" },
                values: new object[,]
                {
                    { 1, "2 PC W/C", 1, "Description", 10m, null, "2Pc Whitefish & Chips", 9m },
                    { 2, "2 PC COD/C", 1, "Description", 12m, null, "2Pc Cod & Chips", 11m },
                    { 3, "2 PC HDK/C", 1, "Description", 14m, null, "2Pc Haddock & Chips", 13m },
                    { 4, "2PC HB/C", 1, "Description", 16m, null, "2Pc Halibut & Chips", 15m }
                });

            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "StaffId", "Email", "Password", "RoleId", "StaffName" },
                values: new object[,]
                {
                    { 1, "asdsad@gmail.com", "Password1", 1, "Staff 1" },
                    { 2, "sad@gmail.com", "Password2", 2, "Staff 2" }
                });

            migrationBuilder.InsertData(
                table: "order",
                columns: new[] { "OrderId", "BeforeTax", "OrderDate", "OrderStatusId", "OrderTypeId", "PaymentStatus", "StaffId", "Tax", "TotalCost", "notes", "table" },
                values: new object[,]
                {
                    { 1, 0m, new DateTime(2023, 3, 31, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3251), 1, 1, false, 1, 0m, 120m, null, "1" },
                    { 2, 0m, new DateTime(2023, 3, 31, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3283), 3, 2, true, 2, 0m, 52m, null, "2" },
                    { 3, 0m, new DateTime(2023, 3, 31, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3285), 2, 1, false, 1, 0m, 0m, null, "3" },
                    { 4, 0m, new DateTime(2023, 3, 31, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3286), 1, 1, false, 1, 0m, 0m, null, null },
                    { 5, 0m, new DateTime(2023, 3, 31, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3287), 1, 1, false, 1, 0m, 0m, null, null },
                    { 6, 0m, new DateTime(2023, 3, 29, 21, 26, 10, 108, DateTimeKind.Local).AddTicks(3289), 2, 3, true, 2, 0m, 523m, null, null }
                });

            migrationBuilder.InsertData(
                table: "productFoodTypes",
                columns: new[] { "ProductId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_foodTypes_ProductId",
                table: "foodTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_order_OrderTypeId",
                table: "order",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_order_StaffId",
                table: "order",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_productFoodTypes_TypeId",
                table: "productFoodTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_products_OrderId",
                table: "products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_staffs_RoleId",
                table: "staffs",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "businessHours");

            migrationBuilder.DropTable(
                name: "orderStatus");

            migrationBuilder.DropTable(
                name: "productFoodTypes");

            migrationBuilder.DropTable(
                name: "websiteInfos");

            migrationBuilder.DropTable(
                name: "foodTypes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "orderType");

            migrationBuilder.DropTable(
                name: "staffs");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
