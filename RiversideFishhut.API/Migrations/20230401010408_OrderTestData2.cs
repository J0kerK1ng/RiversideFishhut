using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiversideFishhut.API.Migrations
{
    public partial class OrderTestData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "staffs");

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4556));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderStatusId" },
                values: new object[] { new DateTime(2023, 3, 31, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4583), 3 });

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4584));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4586));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4588));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "OrderDate",
                value: new DateTime(2023, 3, 29, 21, 4, 7, 709, DateTimeKind.Local).AddTicks(4590));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 1,
                column: "OrderStatusName",
                value: "In Progress");

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 2,
                column: "OrderStatusName",
                value: "Ready");

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 3,
                column: "OrderStatusName",
                value: "Complete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8652));

            migrationBuilder.UpdateData(
                table: "order",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderStatusId" },
                values: new object[] { new DateTime(2023, 3, 31, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8680), 4 });

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
                column: "OrderDate",
                value: new DateTime(2023, 3, 29, 15, 45, 25, 455, DateTimeKind.Local).AddTicks(8709));

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 1,
                column: "OrderStatusName",
                value: "Ordered");

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 2,
                column: "OrderStatusName",
                value: "In Progress");

            migrationBuilder.UpdateData(
                table: "orderStatus",
                keyColumn: "OrderStatusId",
                keyValue: 3,
                column: "OrderStatusName",
                value: "Ready");

            migrationBuilder.InsertData(
                table: "orderStatus",
                columns: new[] { "OrderStatusId", "OrderStatusName" },
                values: new object[] { 4, "Complete" });

            migrationBuilder.UpdateData(
                table: "staffs",
                keyColumn: "StaffId",
                keyValue: 1,
                column: "Description",
                value: "description1");

            migrationBuilder.UpdateData(
                table: "staffs",
                keyColumn: "StaffId",
                keyValue: 2,
                column: "Description",
                value: "description2");
        }
    }
}
