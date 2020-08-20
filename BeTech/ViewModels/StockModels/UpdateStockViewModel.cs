using BeTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.StockModels
{
    public class UpdateStockViewModel : NewStockViewModel
    {
        public UpdateStockViewModel()
        {
            Products = new List<Product>();
            ProductsInStock = new List<Product>();
            SelectedProducts = Array.Empty<int>();
        }

        [Required]
        public int StockId { get; set; }


        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Product> ProductsInStock { get; set; }

        public int[] SelectedProducts { get; set; }

        public IEnumerable<Currency> Currencies { get; set; }

        public Currency CurrentCurrency { get; set; }

        public int CurrencyId { get; set; }

        public decimal Sum { get; set; }
    }
}
