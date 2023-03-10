using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class orderdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "orderStatus",
                columns: new[] { "OrderStatusId", "OrderStatusName" },
                values: new object[,]
                {
                    { 1, "Ordered" },
                    { 2, "In Progress" },
                    { 3, "Ready" },
                    { 4, "Complete" }
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderStatus");

            migrationBuilder.DropTable(
                name: "orderType");
        }
    }
}
