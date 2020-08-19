using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models.ModelFilters
{
    public class StockFilter
    {
        public string SearchString { get; set; }
        public int? ProductId { get; set; }
    }
}
