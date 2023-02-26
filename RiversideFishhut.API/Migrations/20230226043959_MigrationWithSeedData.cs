using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class MigrationWithSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminPassword",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "categories");

            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "AdminId", "AdminAddress", "AdminName", "AdminPassword" },
                values: new object[] { 1, "Admin@gmail.com", "Admin", "Admin123" });

            migrationBuilder.InsertData(
                table: "foodTypes",
                columns: new[] { "TypeId", "Description", "TypeName" },
                values: new object[] { 1, "This type is for 2 Pc fish with 1 pack chip.", "2 PC Dinner" });

            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "StaffId", "Password", "Position", "StaffName" },
                values: new object[,]
                {
                    { 1, "Password1", "Reception", "Staff 1" },
                    { 2, "Password2", "back kitchen", "Staff 2" }
                });

            migrationBuilder.InsertData(
                table: "websiteInfos",
                columns: new[] { "InfoId", "Address", "BusinessHour", "Description", "OnlineOrderLink", "Phone", "StoreName" },
                values: new object[] { 1, "157 king st west Cambridge, ON, N3H 1B5", "Monday closedTuesday 11:30am - 7:30pmWednesday 11:30am - 7:30pmThursday 11:30am - 7:30pmFriday 11:30am - 7:30pmSaturday 11:30am - 8:00pmSunday closed", "This cozy restaurant specializes in traditional English fish and French fries, serving up freshly fried, crispy and flavorful fish sourced from local suppliers, and thick golden fries that make the perfect side dish.", "https://www.skipthedishes.com/riverside-fish-hut?utm_source=riversidefishhutmenu.ca&utm_medium=microsites&utm_campaign=microsites", "519-653-0788", "Riverside Fishhut" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "Description", "TypeName", "foodTypeTypeId" },
                values: new object[] { 1, "Main Dish", "2 PC Dinner", 1 });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "AdminId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "staffs",
                keyColumn: "StaffId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "staffs",
                keyColumn: "StaffId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "websiteInfos",
                keyColumn: "InfoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "foodTypes",
                keyColumn: "TypeId",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "AdminPassword",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeId",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
