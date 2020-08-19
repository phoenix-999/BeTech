﻿using BeTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
        }


        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockProduct> StockProduct { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(CurrencyConfig);
            modelBuilder.Entity<StockProduct>(StockProductConfig);
        }


        protected virtual void CurrencyConfig(EntityTypeBuilder<Currency> builder)
        {
            var defaultData = new List<Currency>
            {
                new Currency
                {
                    CurrencyId = 1,
                    Code = "USD",
                    Rate = 1,
                    UpdateTime = DateTime.Now,
                    Factor = 1,
                    IsBaseCurrencyType = true
                },
                new Currency
                {
                    CurrencyId = 2,
                    Code = "EURO",
                    Rate = 1,
                    Factor = 1.1m,
                    UpdateTime = DateTime.Now
                },
                new Currency
                {
                    CurrencyId = 3,
                    Code = "UAH",
                    Rate = 1,
                    Factor = 0.28m,
                    UpdateTime = DateTime.Now
                }
            };
            builder.HasData(defaultData);
        }
        
        
        protected virtual void StockProductConfig(EntityTypeBuilder<StockProduct> builder)
        {
            builder.HasKey(sp => new { sp.ProductId, sp.StockId });

            builder
                .HasOne(sp => sp.Product)
                .WithMany(p => p.StockProduct)
                .HasForeignKey(sp => sp.ProductId);

            builder
                .HasOne(sp => sp.Stock)
                .WithMany(s => s.StockProduct)
                .HasForeignKey(sp => sp.StockId);
        }


    }
}
