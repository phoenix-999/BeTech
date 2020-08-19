using BeTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.ProductModels
{
    public class NewProductViewModel
    {
        public NewProductViewModel()
        {
            Categories = new List<ProductCategory>();
            Stocks = new List<Stock>();
        }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IEnumerable<Currency>  Currencies { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public IEnumerable<ProductCategory> Categories { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Stock> Stocks { get; set; }

        public IEnumerable<Stock> CurrentStocks { get; set; }

        [Required]
        public int[] SelectedStocks { get; set; }
    }
}
