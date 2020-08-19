using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models.ModelFilters
{
    public class ProductFilter
    {
        public string SearchString { get; set; }

        public int? ProductCategoryId { get; set; }

        public int? StockId { get; set; }
    }
}
