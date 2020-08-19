using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class StockProduct
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public double? Count { get; set; }
    }
}
