using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.StockModels
{
    public class NewStockViewModel
    {
        [Required]
        public string StockName { get; set; }

        public string Address { get; set; }
    }
}
