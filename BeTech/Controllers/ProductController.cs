using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using BeTech.Data.Repositories;
using BeTech.Models;
using BeTech.Models.ModelFilters;
using BeTech.ViewModels;
using BeTech.ViewModels.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZXing.QrCode.Internal;

namespace BeTech.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ViewModelHelper _viewModelHelper;

        public ProductController(IProductsRepository productRepository, ICurrencyRepository currencyRepository, IStockRepository stockRepository, ViewModelHelper viewModelHelper)
        {
            _productRepository = productRepository;
            _currencyRepository = currencyRepository;
            _stockRepository = stockRepository;
            _viewModelHelper = viewModelHelper;
        }


        #region Categories

        public IActionResult Categories(CategoryFilter filter)
        {
            var categories = _productRepository.ProductCategories
                .Where(pc => filter.SearchString == default || pc.CategoryName.ToLower().Contains(filter.SearchString.ToLower()))
                .Where(pc => filter.ProductId == default || pc.Products.Where(p => p.ProductId == filter.ProductId).Count() > 0)
                .Include(pc => pc.Products);

            return View(categories);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(NewProductCategoryViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await _productRepository.AddProductCategoryAsync(model.CategoryName);

            return RedirectToAction(nameof(Categories));
        }


        [HttpGet]
        public IActionResult EditCategory([Required]int categoryId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = _productRepository.ProductCategories.Where(pc => pc.CategoryId == categoryId).FirstOrDefault();
            if (category == null) return NotFound();
            var model = new UpdateProductCategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditCategory(UpdateProductCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var updateResult = await _productRepository.UpdateProductCategoryAsync(model.CategoryId, model.CategoryName);
            if (updateResult == null) return NotFound();
            return RedirectToAction(nameof(Categories));
        }


        [HttpGet]
        public IActionResult DeleteCategory([Required] int categoryId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var category = _productRepository.ProductCategories.Where(pc => pc.CategoryId == categoryId).SingleOrDefault();
            if (category == null) return NotFound();
            var model = new DeleteProductCategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(DeleteProductCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var deleteResult = await _productRepository.DeleteProductCategoryAsync(model.CategoryId);
            if (deleteResult == null) return NotFound();
            return RedirectToAction(nameof(Categories));
        }

        #endregion

        #region Product

        public IActionResult Products(ProductFilter filter)
        {
            var products = _productRepository.Products
                            .Where(p => filter.SearchString == default || p.ProductName.ToLower().Contains(filter.SearchString.ToLower()))
                            .Where(p => filter.ProductCategoryId == default || p.CategoryId == filter.ProductCategoryId)
                            .Where(p => filter.StockId == default || p.StockProduct.Where(p => p.StockId == filter.StockId).Count() > 0)
                            .Include(p => p.Category)
                            .Include(p => p.Currency);

            ViewBag.BaseCurrency = _currencyRepository.GetBaseCurrency();
            return View(products);
        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            var model = _viewModelHelper.GetNewProductViewModel();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(NewProductViewModel model)
        {
            if (!ModelState.IsValid) return View(_viewModelHelper.GetNewProductViewModel(model));

            await _productRepository.AddProductAsync(
                productName: model.ProductName,
                price: model.Price,
                categoryId: model.CategoryId,
                currencyId: model.CurrencyId,
                stocksId: model.SelectedStocks);

            return RedirectToAction(nameof(Products));
        }


        [HttpGet]
        public IActionResult EditProduct([Required] int productId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var product = _productRepository.Products
                            .Where(p => p.ProductId == productId)
                            .Include(p => p.Currency)
                            .Include(p => p.Category)
                            .Include(p => p.StockProduct).ThenInclude(sp => sp.Stock)
                            .SingleOrDefault();

            if (product == null) return NotFound();

            var model = new UpdateProductViewModel()
            {
                ProductId = product.ProductId,
                Barcode = product.Barcode,
                CategoryId = product.CategoryId,
                CurrencyId = product.CurrencyId,
                CurrentStocks = product.StockProduct.Select(sp => sp.Stock).ToList(),
                Price = product.Price,
                ProductName = product.ProductName,
                CurrentCurrency = product.Currency,
                CurrentCategory = product.Category
            };
            model = _viewModelHelper.GetUpdateProductViewModel(model);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductViewModel model)
        {
            if (!ModelState.IsValid) return View(_viewModelHelper.GetUpdateProductViewModel(model));

            var updateResult = await _productRepository.UpdateProductAsync(
                productId: model.ProductId,
                productName: model.ProductName,
                price: model.Price,
                categoryId: model.CategoryId,
                currencyId: model.CurrencyId,
                stocksId: model.SelectedStocks);

            if (updateResult == null) return NotFound();

            return RedirectToAction(nameof(Products));
        }


        [HttpGet]
        public IActionResult DeleteProduct([Required] int productId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var product = _productRepository.Products.Where(p => p.ProductId == productId).SingleOrDefault();
            if (product == null) return NotFound();
            var model = new DeleteProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var deleteResult = await _productRepository.DeleteProductAsync(model.ProductId);
            if (deleteResult == null) return NotFound();
            return RedirectToAction(nameof(Products));
        }

        #endregion
    }
}
