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
