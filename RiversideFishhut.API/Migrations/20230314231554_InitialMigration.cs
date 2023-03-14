using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "businessHours",
                columns: table => new
                {
                    BusinessHourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businessTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessHours", x => x.BusinessHourId);
                });

            migrationBuilder.CreateTable(
                name: "foodTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "websiteInfos",
                columns: table => new
                {
                    InfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlineOrderLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_websiteInfos", x => x.InfoId);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_categories_foodTypes_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "foodTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFoodTypes",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FoodTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFoodTypes", x => new { x.CategoryId, x.FoodTypeId });
                    table.ForeignKey(
                        name: "FK_CategoryFoodTypes_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFoodTypes_foodTypes_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "foodTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dine_in_price = table.Column<int>(type: "int", nullable: false),
                    Take_out_price = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "AdminAddress", "AdminName", "AdminPassword" },
                values: new object[] { 1, "Admin@gmail.com", "Admin", "Admin123" });

            migrationBuilder.InsertData(
                table: "businessHours",
                columns: new[] { "BusinessHourId", "businessTime", "dayOfWeek" },
                values: new object[] { 1, "09:00 - 17:00", "Monday" });

            migrationBuilder.InsertData(
                table: "foodTypes",
                columns: new[] { "TypeId", "Description", "TypeName" },
                values: new object[,]
                {
                    { 1, "This type is for 2 Pc fish with 1 pack chip.", "2 PC Dinner" },
                    { 2, "Description for FoodType2", "FoodType2" },
                    { 3, "Description for FoodType3", "FoodType3" },
                    { 4, "Description for FoodType4", "FoodType4" }
                });

            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "StaffId", "Description", "Password", "StaffName", "roleId" },
                values: new object[,]
                {
                    { 1, "description1", "Password1", "Staff 1", "Admin" },
                    { 2, "description2", "Password2", "Staff 2", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "websiteInfos",
                columns: new[] { "InfoId", "Address", "Description", "OnlineOrderLink", "Phone", "StoreName" },
                values: new object[] { 1, "157 king st west Cambridge, ON, N3H 1B5", "This cozy restaurant specializes in traditional English fish and French fries, serving up freshly fried, crispy and flavorful fish sourced from local suppliers, and thick golden fries that make the perfect side dish.", "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites", "519-653-0788", "Riverside Fishhut" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "Description", "FoodTypeId", "TypeName" },
                values: new object[] { 1, "Main Dish", 1, "2 PC Dinner" });

            migrationBuilder.InsertData(
                table: "CategoryFoodTypes",
                columns: new[] { "CategoryId", "FoodTypeId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Dine_in_price", "ProductName", "Take_out_price" },
                values: new object[,]
                {
                    { 1, 1, 10, "2Pc Whitefish & Chips", 9 },
                    { 2, 1, 12, "2Pc Cod & Chips", 11 },
                    { 3, 1, 14, "2Pc Haddock & Chips", 13 },
                    { 4, 1, 16, "2Pc Halibut & Chips", 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_FoodTypeId",
                table: "categories",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFoodTypes_FoodTypeId",
                table: "CategoryFoodTypes",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "businessHours");

            migrationBuilder.DropTable(
                name: "CategoryFoodTypes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "staffs");

            migrationBuilder.DropTable(
                name: "websiteInfos");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "foodTypes");
        }
    }
}
