using BeTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data.Repositories
{
    public interface IStockRepository
    {
        IQueryable<Stock> Stocks { get; }
        Task<Stock> AddStockAsync(string stockName, string address);
        Task<Stock> UpdateStockAsync(int stockId, string stockName, string address, int[] products);
        Task<Stock> DeleteStockAsync(int stockId);
    }


    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<Stock> Stocks => _context.Stocks.AsNoTracking().Where(s => s.Deleted != true);


        public async Task<Stock> AddStockAsync(string stockName, string address)
        {
            var stock = new Stock
            {
                StockName = stockName,
                Address = address
            };
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }


        public async Task<Stock> UpdateStockAsync(int stockId, string stockName, string address, int[] products)
        {
            var stock = _context.Stocks.Where(p => p.StockId == stockId).SingleOrDefault();
            if (stock == null) return null;

            stock.StockName = stockName;
            stock.Address = address;

            if (products != null)
            {
                var currentProducts = _context.StockProduct.Where(sp => sp.StockId == stockId).Select(sp => sp.ProductId).ToArray();
                var addedProducts = products.Except(currentProducts);
                var removeProducts = currentProducts.Except(products);
                _context.StockProduct.RemoveRange(_context.StockProduct.Where(sp => sp.StockId == stock.StockId && removeProducts.Contains(sp.ProductId)));
                if (addedProducts.Count() > 0)
                {
                    foreach (var id in addedProducts)
                    {
                        _context.StockProduct.Add(new StockProduct { StockId = stock.StockId, ProductId = id });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return stock;
        }


        public async Task<Stock> DeleteStockAsync(int stockId)
        {
            var stock = await _context.Stocks.AsNoTracking().Where(s => s.StockId == stockId).SingleOrDefaultAsync();
            if (stock == null) return null;
            stock.Deleted = true;
            _context.Entry(stock).Property(s => s.Deleted).IsModified = true;
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}
