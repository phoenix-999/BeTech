using BeTech.Data.Repositories;
using BeTech.Models.ModelFilters;
using BeTech.ViewModels.StockModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository) 
        {
            _stockRepository = stockRepository;
        }


        public IActionResult Stocks(StockFilter filter)
        {
            var stocks = _stockRepository.Stocks
                .Where(s => filter.SearchString == default || s.StockName.ToLower().Contains(filter.SearchString.ToLower()))
                .Where(s => filter.ProductId == default || s.StockProduct.Where(p => p.ProductId == filter.ProductId).Count() > 0)
                .Include(s => s.StockProduct).ThenInclude(sp => sp.Product);

            return View(stocks);
        }


        [HttpGet]
        public IActionResult AddStock()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddStock(NewStockViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _stockRepository.AddStockAsync(stockName: model.StockName, address: model.Address);
            return RedirectToAction(nameof(Stocks));
        }


        [HttpGet]
        public IActionResult DeleteStock([Required] int stockId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var stock = _stockRepository.Stocks.Where(s => s.StockId == stockId).SingleOrDefault();
            if (stock == null) return NotFound();

            var model = new DeleteStockViewModel
            {
                StockId = stock.StockId,
                StockName = stock.StockName
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteStock(DeleteStockViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var deletedStock = await _stockRepository.DeleteStockAsync(model.StockId);
            if (deletedStock == null) return NotFound();

            return RedirectToAction(nameof(Stocks));
        }
    }
}
