using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class MigrationWithData : Migration
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
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_admins_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                table: "roles",
                columns: new[] { "RoleId", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Staff member", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "websiteInfos",
                columns: new[] { "InfoId", "Address", "Description", "LogoImage", "OnlineOrderLink", "PhoneNumber", "StoreName" },
                values: new object[] { 1, "157 king st west Cambridge, ON, N3H 1B5", "This cozy restaurant specializes in traditional English fish and French fries, serving up freshly fried, crispy and flavorful fish sourced from local suppliers, and thick golden fries that make the perfect side dish.", "Logo name", "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites", "519-653-0788", "Riverside Fishhut" });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "AdminEmailAddress", "AdminName", "AdminPassword", "RoleId" },
                values: new object[] { 1, "Admin@gmail.com", "Admin", "Admin123", 1 });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "AltName", "CategoryId", "Description", "Dine_in_price", "ProductName", "Take_out_price" },
                values: new object[,]
                {
                    { 1, "2 PC W/C", 1, "Description", 10m, "2Pc Whitefish & Chips", 9m },
                    { 2, "2 PC COD/C", 1, "Description", 12m, "2Pc Cod & Chips", 11m },
                    { 3, "2 PC HDK/C", 1, "Description", 14m, "2Pc Haddock & Chips", 13m },
                    { 4, "2PC HB/C", 1, "Description", 16m, "2Pc Halibut & Chips", 15m }
                });

            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "StaffId", "Description", "Password", "RoleId", "StaffName" },
                values: new object[,]
                {
                    { 1, "description1", "Password1", 1, "Staff 1" },
                    { 2, "description2", "Password2", 2, "Staff 2" }
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
                name: "IX_admins_RoleId",
                table: "admins",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_foodTypes_ProductId",
                table: "foodTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_productFoodTypes_TypeId",
                table: "productFoodTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_staffs_RoleId",
                table: "staffs",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "businessHours");

            migrationBuilder.DropTable(
                name: "productFoodTypes");

            migrationBuilder.DropTable(
                name: "staffs");

            migrationBuilder.DropTable(
                name: "websiteInfos");

            migrationBuilder.DropTable(
                name: "foodTypes");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
