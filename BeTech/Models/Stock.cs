using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        [Required]
        [MaxLength(200)]
        public string StockName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public List<StockProduct> StockProduct { get; set; }
    }
}
