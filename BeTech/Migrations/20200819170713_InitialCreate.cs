using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeTech.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 4, nullable: true),
                    Rate = table.Column<decimal>(type: "money", nullable: false),
                    Factor = table.Column<decimal>(type: "money", nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    IsBaseCurrencyType = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockName = table.Column<string>(maxLength: 200, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(maxLength: 200, nullable: false),
                    PriceInBaseCurrency = table.Column<decimal>(type: "money", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Barcode = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockProduct",
                columns: table => new
                {
                    StockId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Count = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockProduct", x => new { x.ProductId, x.StockId });
                    table.ForeignKey(
                        name: "FK_StockProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockProduct_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "Factor", "IsBaseCurrencyType", "Rate", "UpdateTime" },
                values: new object[] { 1, "USD", 1m, true, 1m, new DateTime(2020, 8, 19, 20, 7, 13, 244, DateTimeKind.Local).AddTicks(1754) });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "Factor", "IsBaseCurrencyType", "Rate", "UpdateTime" },
                values: new object[] { 2, "EURO", 1.1m, false, 1m, new DateTime(2020, 8, 19, 20, 7, 13, 247, DateTimeKind.Local).AddTicks(1754) });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "Factor", "IsBaseCurrencyType", "Rate", "UpdateTime" },
                values: new object[] { 3, "UAH", 0.28m, false, 1m, new DateTime(2020, 8, 19, 20, 7, 13, 247, DateTimeKind.Local).AddTicks(1754) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockProduct_StockId",
                table: "StockProduct",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockProduct");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
