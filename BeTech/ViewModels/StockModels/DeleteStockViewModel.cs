using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.StockModels
{
    public class DeleteStockViewModel
    {
        [Required]
        public int StockId { get; set; }

        public string StockName { get; set; }
    }
}
