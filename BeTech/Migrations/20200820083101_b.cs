using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 31, 1, 219, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 31, 1, 222, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 3,
                column: "UpdateTime",
                value: new DateTime(2020, 8, 20, 11, 31, 1, 222, DateTimeKind.Local).AddTicks(1754));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
