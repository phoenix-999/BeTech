using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.CurrencyModels
{
    public class DeleteCurrencyViewModel
    {
        [Required]
        public int CurrencyId { get; set; }

        public string CurrencyName { get; set; }
    }
}
