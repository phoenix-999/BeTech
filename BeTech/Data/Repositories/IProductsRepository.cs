using BeTech.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace BeTech.Data.Repositories
{
    public interface IProductsRepository
    {
        IQueryable<ProductCategory> ProductCategories { get; }
        IQueryable<Product> Products { get; }
        Task<ProductCategory> AddProductCategoryAsync(string name);
        Task<ProductCategory> UpdateProductCategoryAsync(int categoryId, string name);
        Task<ProductCategory> DeleteProductCategoryAsync(int categoryId);
        Task<Product> AddProductAsync(string productName, decimal price, int categoryId, int currencyId, int[] stocksId);
        Task<Product> UpdateProductAsync(int productId, string productName, decimal price, int categoryId, int currencyId, int[] stocksId);
        Task<Product> DeleteProductAsync(int productId);
        Task UpdateBasePrices();
    }


    public class ProductRepository : IProductsRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStockRepository _stockRepository;

        public ProductRepository(AppDbContext context, ICurrencyRepository currencyRepository, IStockRepository stockRepository)
        {
            _context = context;
            _currencyRepository = currencyRepository;
            _stockRepository = stockRepository;
        }


        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories.AsNoTracking().Where(pc => pc.Deleted != true);

        public IQueryable<Product> Products => _context.Products.AsNoTracking().Where(p => p.Deleted != true);


        public async Task<ProductCategory> AddProductCategoryAsync(string name)
        {
            var category = new ProductCategory { CategoryName = name };
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<ProductCategory> UpdateProductCategoryAsync(int categoryId, string name)
        {
            var category = await _context.ProductCategories.Where(pc => pc.CategoryId == categoryId).SingleOrDefaultAsync();
            if (category == null) return null;
            category.CategoryName = name;
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<ProductCategory> DeleteProductCategoryAsync(int categoryId)
        {
            var category = await _context.ProductCategories.AsNoTracking().Where(pc => pc.CategoryId == categoryId).SingleOrDefaultAsync();
            if (category == null) return null;
            category.Deleted = true;
            _context.Entry(category).Property(c => c.Deleted).IsModified = true;
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<Product> AddProductAsync(string productName, decimal price, int categoryId, int currencyId, int[] stocksId)
        {
            var currentCurrency = _currencyRepository.Currencies.Where(c => c.CurrencyId == currencyId).Single();


            var barcodeValue = GetBarcodeValue(8);

            var product = new Product
            {
                ProductName = productName,
                Price = price,
                CategoryId = categoryId,
                CurrencyId = currencyId,
                PriceInBaseCurrency = price * currentCurrency.Rate,
                BarcodeValue = barcodeValue,
                Barcode = CreateBarCode(barcodeValue.ToString())
            };

            _context.Products.Add(product);

            foreach (var stock in stocksId)
            {
                _context.StockProduct.Add(new StockProduct
                {
                    Product = product,
                    StockId = stock
                });
            }

            await  _context.SaveChangesAsync();

           return product;
        }


        public async Task<Product> UpdateProductAsync(int productId, string productName, decimal price, int categoryId, int currencyId, int[] stocksId)
        {
            var currentCurrency = _currencyRepository.Currencies.Where(c => c.CurrencyId == currencyId).Single();
            var product = _context.Products.Where(p => p.ProductId == productId).SingleOrDefault();
            if (product == null) return null;

            product.ProductName = productName;
            product.Price = price;
            product.CategoryId = categoryId;
            product.CurrencyId = currencyId;
            product.PriceInBaseCurrency = price * currentCurrency.Rate;

            if (stocksId != null)
            {
                var currentStocks = _context.StockProduct.Where(sp => sp.ProductId == productId).Select(sp => sp.StockId).ToArray();
                var addedStocks = stocksId.Except(currentStocks);
                var removeStocks = currentStocks.Except(stocksId);
                _context.StockProduct.RemoveRange(_context.StockProduct.Where(sp => removeStocks.Contains(sp.StockId)));

                if (addedStocks.Count() > 0)
                {
                    foreach (var id in addedStocks)
                    {
                        _context.StockProduct.Add(new StockProduct { ProductId = product.ProductId, StockId = id });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return product;

        }


        public async Task<Product> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.Where(p => p.ProductId == productId).SingleOrDefaultAsync();
            if (product == null) return null;
            product.Deleted = true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task UpdateBasePrices()
        {
            var baseCurrency = _currencyRepository.GetBaseCurrency();
            foreach(var product in _context.Products.Include(p => p.Currency))
            {
                product.PriceInBaseCurrency = product.Price * product.Currency.Rate;
            }
            await _context.SaveChangesAsync();
        }


        private byte[] CreateBarCode(string content)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.ImageFormat = ImageFormat.Png;
            Image img = b.Encode(BarcodeLib.TYPE.CODE128, content, Color.Black, Color.White, 300, 100);
            

            ImageConverter imageConverter = new ImageConverter();
            byte[] result = (byte[])imageConverter.ConvertTo(img, typeof(byte[]));
            return result;
        }


        private string GetBarcodeValue(int length)
        {
            var barcodeValue = "";
            for (int i = 0; i < length; i++)
            {
                var rand = new Random();
                barcodeValue += (char)rand.Next(97, 122);
            }

            var exists = _context.Products.AsNoTracking().Where(p => p.BarcodeValue == barcodeValue).Any();
            if(exists)
            {
                barcodeValue = GetBarcodeValue(length);
            }

            return barcodeValue;
        }
    }
}
