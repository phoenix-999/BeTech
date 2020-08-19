using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BeTech.Data.Repositories;
using BeTech.Models;
using BeTech.Models.ModelFilters;
using BeTech.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeTech.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productRepository;

        public ProductController(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Categories(CategoryFilter filter)
        {
            var categories = _productRepository.ProductCategories.Include(pc => pc.Products)
                .Where(pc => filter.SearchString == default || pc.CategoryName.ToLower().Contains(filter.SearchString.ToLower()))
                .Where(pc => filter.ProductId == default || pc.Products.Where(p => p.ProductId == filter.ProductId).Count() > 0);

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
                return View(nameof(EditCategory), model);
            }

            await _productRepository.AddProductCategory(model.CategoryName);

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
            if(!ModelState.IsValid) return BadRequest();
            var updateResult = await _productRepository.UpdateProductCategory(model.CategoryId, model.CategoryName);
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
            if (!ModelState.IsValid) return BadRequest();
            var deleteResult = await _productRepository.DeleteProductCategory(model.CategoryId);
            if (deleteResult == null) return NotFound();
            return RedirectToAction(nameof(Categories));
        }
    }
}
