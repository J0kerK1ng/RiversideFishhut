using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class OrderData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "order",
                columns: new[] { "OrderId", "OrderDate", "OrderStatusId", "OrderTypeId", "PaymentStatus", "StaffId", "notes", "table" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3552), 1, 1, false, 1, null, null },
                    { 2, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3581), 4, 2, true, 2, null, null },
                    { 3, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3583), 2, 1, false, 1, null, null },
                    { 4, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3585), 1, 1, false, 1, null, null },
                    { 5, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3586), 1, 1, false, 1, null, null },
                    { 6, new DateTime(2023, 3, 15, 13, 55, 15, 629, DateTimeKind.Local).AddTicks(3588), 2, 3, true, 2, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 6);
        }
    }
}
