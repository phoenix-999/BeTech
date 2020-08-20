using BeTech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels.CurrencyModels
{
    public class UpdateCurrencyViewModel : NewCurrencyViewModel
    {
        public UpdateCurrencyViewModel()
        {

        }

        public UpdateCurrencyViewModel(Currency currency)
        {
            CurrencyId = currency.CurrencyId;
            CurrencyName = currency.CurrencyName;
            Rate = currency.Rate;
            Code = currency.Code;
        }

        [Required]
        public int CurrencyId { get; set; }
    }
}
