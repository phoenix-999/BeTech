using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BarcodeValue",
                table: "Products",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 17, 47, 467, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 17, 47, 470, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 17, 47, 470, DateTimeKind.Local).AddTicks(1754));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "BarcodeValue",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 12, 25, 594, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 12, 25, 597, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 12, 25, 597, DateTimeKind.Local).AddTicks(1754));
        }
    }
}
