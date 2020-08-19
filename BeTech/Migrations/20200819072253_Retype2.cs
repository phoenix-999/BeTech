using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class Retype2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "Stocks",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stocks",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "ProductCategories",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 0,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 22, 53, 38, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 22, 53, 40, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 22, 53, 40, DateTimeKind.Local).AddTicks(1754));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StockName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 0,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 21, 45, 754, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 21, 45, 756, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 21, 45, 756, DateTimeKind.Local).AddTicks(1754));
        }
    }
}
