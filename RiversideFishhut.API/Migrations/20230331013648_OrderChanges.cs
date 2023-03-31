using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class OrderChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderLineItem");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7647), "1" });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7674), "2" });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7675), "3" });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "OrderDate",
                value: new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "OrderDate",
                value: new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "OrderDate",
                value: new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.CreateIndex(
                name: "IX_products_OrderId",
                table: "products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_order_OrderId",
                table: "products",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_order_OrderId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_OrderId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "products");

            migrationBuilder.CreateTable(
                name: "orderLineItem",
                columns: table => new
                {
                    OrderLineItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderLineItem", x => x.OrderLineItemId);
                    table.ForeignKey(
                        name: "FK_orderLineItem_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9284), null });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9307), null });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3,
                columns: new[] { "OrderDate", "table" },
                values: new object[] { new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9308), null });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "OrderDate",
                value: new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "OrderDate",
                value: new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9312));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "OrderDate",
                value: new DateTime(2023, 3, 29, 11, 37, 49, 95, DateTimeKind.Local).AddTicks(9313));

            migrationBuilder.CreateIndex(
                name: "IX_orderLineItem_ProductId",
                table: "orderLineItem",
                column: "ProductId");
        }
    }
}
