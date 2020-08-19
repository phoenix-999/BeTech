using BeTech.Models;
using Microsoft.EntityFrameworkCore;
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
                    CurrencyType = CurrencyTypes.USD,
                    Code = CurrencyTypes.USD.ToString(),
                    Rate = 1,
                    UpdateTime = DateTime.Now,
                    IsBaseCurrency = true
                },
                new Currency
                {
                    CurrencyType = CurrencyTypes.EURO,
                    Code = CurrencyTypes.EURO.ToString(),
                    Rate = 1,
                    UpdateTime = DateTime.Now
                },
                new Currency
                {
                    CurrencyType = CurrencyTypes.UAH,
                    Code = CurrencyTypes.UAH.ToString(),
                    Rate = 1,
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
    

        protected virtual void ProductConfig(EntityTypeBuilder<Product> builder)
        {
            //string createTriggerSqlCommand = @"
            //    create trigger TR_Table__AU on [Products] after update as
            //    if exists(
            //      select 1 
            //      from inserted i
            //      join deleted d on d.doc_id=i.doc_id -- по первичному ключу Table
            //      where datediff(day,i.date_doc,getdate())>1 -- сутки без учета времени
            //        and i.Quantity1 != isnull(d.Quantity1,0) -- изменено количество
            //      )
            //    rollback tran;
            //    GO
            //";
            //Database.ExecuteSqlRaw(createTriggerSqlCommand);
        }
    }
}
