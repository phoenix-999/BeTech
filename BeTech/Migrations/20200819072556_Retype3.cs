using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class Retype3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Currencies",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 0,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 25, 55, 632, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 25, 55, 635, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 25, 55, 635, DateTimeKind.Local).AddTicks(1754));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

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
    }
}
