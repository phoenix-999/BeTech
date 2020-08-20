using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.CurrencyModels
{
    public class NewCurrencyViewModel
    {
        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public string Code { get; set; }

        public decimal Rate { get; set; }
    }
}
