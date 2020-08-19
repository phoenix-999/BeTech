using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class Currency
    {
        public Currency()
        {
            IsBaseCurrency = false;
        }


        [Key]
        public CurrencyTypes CurrencyType { get; set; }

        [MaxLength(4)]
        public string Code { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        public DateTime UpdateTime { get; set; }
        public bool IsBaseCurrency { get; set; }

    }

    public enum CurrencyTypes
    {
        USD,
        EURO,
        UAH
    }

}
