﻿// <auto-generated />
using System;
using BeTech.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeTech.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200819072706_Retype4")]
    partial class Retype4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeTech.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<bool>("IsBaseCurrency")
                        .HasColumnType("bit");

                    b.Property<decimal>("Rate")
                        .HasColumnType("money");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("CurrencyType");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            CurrencyType = 0,
                            Code = "USD",
                            IsBaseCurrency = true,
                            Rate = 1m,
                            UpdateTime = new DateTime(2020, 8, 19, 10, 27, 6, 471, DateTimeKind.Local).AddTicks(1754)
                        },
                        new
                        {
                            CurrencyType = 1,
                            Code = "EURO",
                            IsBaseCurrency = false,
                            Rate = 1m,
                            UpdateTime = new DateTime(2020, 8, 19, 10, 27, 6, 474, DateTimeKind.Local).AddTicks(1754)
                        },
                        new
                        {
                            CurrencyType = 2,
                            Code = "UAH",
                            IsBaseCurrency = false,
                            Rate = 1m,
                            UpdateTime = new DateTime(2020, 8, 19, 10, 27, 6, 474, DateTimeKind.Local).AddTicks(1754)
                        });
                });

            modelBuilder.Entity("BeTech.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<decimal>("PriceInBaseCurrency")
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CurrencyType");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BeTech.Models.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("BeTech.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("StockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("BeTech.Models.StockProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.HasKey("ProductId", "StockId");

                    b.HasIndex("StockId");

                    b.ToTable("StockProduct");
                });

            modelBuilder.Entity("BeTech.Models.Product", b =>
                {
                    b.HasOne("BeTech.Models.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeTech.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeTech.Models.StockProduct", b =>
                {
                    b.HasOne("BeTech.Models.Product", "Product")
                        .WithMany("StockProduct")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeTech.Models.Stock", "Stock")
                        .WithMany("StockProduct")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
