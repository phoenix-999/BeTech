using BeTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data.Repositories
{
    public interface IProductsRepository
    {
        IQueryable<ProductCategory> ProductCategories { get; }
        Task<ProductCategory> AddProductCategory(string name);
        Task<ProductCategory> UpdateProductCategory(int categoryId, string name);
        Task<ProductCategory> DeleteProductCategory(int categoryId);
    }


    public class ProductRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<ProductCategory> ProductCategories => _context.ProductCategories.AsNoTracking().Where(pc => pc.Deleted != true);


        public async Task<ProductCategory> AddProductCategory(string name)
        {
            var category = new ProductCategory { CategoryName = name };
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<ProductCategory> UpdateProductCategory(int categoryId, string name)
        {
            var category = await _context.ProductCategories.AsNoTracking().Where(pc => pc.CategoryId == categoryId).SingleOrDefaultAsync();
            if (category == null) return null;
            category.CategoryName = name;
            _context.Entry(category).Property(c => c.CategoryName).IsModified = true;
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<ProductCategory> DeleteProductCategory(int categoryId)
        {
            var category = await _context.ProductCategories.AsNoTracking().Where(pc => pc.CategoryId == categoryId).SingleOrDefaultAsync();
            if (category == null) return null;
            category.Deleted = true;
            _context.Entry(category).Property(c => c.Deleted).IsModified = true;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
