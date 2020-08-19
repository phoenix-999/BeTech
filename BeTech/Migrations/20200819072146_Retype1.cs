using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class Retype1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Currencies",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 0,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 20, 8, 863, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 20, 8, 865, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyType",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 19, 10, 20, 8, 866, DateTimeKind.Local).AddTicks(1754));
        }
    }
}
