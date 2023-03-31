using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class OrderTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8652), 120m });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8680), 52m });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8685));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 6,
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 29, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8709), 523m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7647), 0m });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7674), 0m });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7675));

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
                columns: new[] { "OrderDate", "TotalCost" },
                values: new object[] { new DateTime(2023, 3, 30, 21, 36, 48, 297, DateTimeKind.Local).AddTicks(7680), 0m });
        }
    }
}
