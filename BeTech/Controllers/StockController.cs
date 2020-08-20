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
using BeTech.ViewModels;
using BeTech.Models;

namespace BeTech.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ViewModelHelper _viewModelHelper;

        public StockController(IStockRepository stockRepository, IProductsRepository productsRepository, ViewModelHelper viewModelHelper, ICurrencyRepository currencyRepository) 
        {
            _stockRepository = stockRepository;
            _productsRepository = productsRepository;
            _viewModelHelper = viewModelHelper;
            _currencyRepository = currencyRepository;
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
        public IActionResult EditStock([Required] int stockId, int? currencyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var stock = _stockRepository.Stocks
                            .Where(s => s.StockId == stockId)
                            .Include(s => s.StockProduct).ThenInclude(sp => sp.Product)
                            .SingleOrDefault();
            if (stock == null) return NotFound();

            var model = _viewModelHelper.GetUpdateStockViewModel();
            model.StockId = stock.StockId;
            model.StockName = stock.StockName;
            model.Address = stock.Address;
            model.ProductsInStock = stock.StockProduct.Select(sp => sp.Product);
            if (currencyId.HasValue)
            {
                model.CurrencyId = currencyId.Value;
                
                var currency = _currencyRepository.Currencies.Where(c => c.CurrencyId == currencyId).SingleOrDefault();
                if (currency != default)
                {
                    model.CurrentCurrency = currency;
                    model.Sum = stock.StockProduct.Select(sp => sp.Product.Price * currency.Rate).Sum();
                }
             }
            else
            {
                var currency = _currencyRepository.GetBaseCurrency();
                model.CurrentCurrency = currency;
                model.CurrencyId = currency.CurrencyId;
                model.Sum = stock.StockProduct.Select(sp => sp.Product.PriceInBaseCurrency).Sum();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditStock(UpdateStockViewModel model, bool? getProducts)
        {
            if (!ModelState.IsValid)
            {
                model = _viewModelHelper.GetUpdateStockViewModel(model);
                model.ProductsInStock = _productsRepository.Products.Where(p => model.SelectedProducts.Contains(p.ProductId));
                return View(model);
            }

            var updateResult = await _stockRepository.UpdateStockAsync(
                stockId: model.StockId,
                stockName: model.StockName,
                address: model.Address,
                products: model.SelectedProducts
                );
            if (updateResult == null) return NotFound();

            if(getProducts == true)
            {
                return RedirectToAction(nameof(ProductsInStock), new { stockId = model.StockId});
            }

            return RedirectToAction(nameof(Stocks));
        }

        [HttpGet]
        public IActionResult ProductsInStock([Required]int stockId)
        {
            if (!ModelState.IsValid) return BadRequest();
            bool existsStock = _stockRepository.Stocks.Where(s => s.StockId == stockId).Any();
            if (!existsStock) return NotFound();

            var stockProducts = _stockRepository.StocksProducts
                                    .Where(sp => sp.StockId == stockId)
                                    .Include(sp => sp.Stock)
                                    .Include(sp => sp.Product);
            ViewBag.stockId = stockId;
            return View(stockProducts);
        }


        [HttpPost]
        public async Task<IActionResult> ProductsInStock([Required] int stockId, StockProduct stockProduct)
        {
            if (!ModelState.IsValid)
            {
                var stockProducts = _stockRepository.StocksProducts
                                    .Where(sp => sp.StockId == stockId)
                                    .Include(sp => sp.Stock)
                                    .Include(sp => sp.Product);
                return View(stockProducts);
            }

            await _stockRepository.UpdateStockProductAsync(stockProduct.StockId, stockProduct.ProductId, stockProduct.Count);

            return RedirectToAction(nameof(EditStock), new { stockId });
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
