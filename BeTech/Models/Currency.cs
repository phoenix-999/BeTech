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
        public int CurrencyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CurrencyName { get; set; }

        [Required]
        [MaxLength(4)]
        public string Code { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool IsBaseCurrencyType { get; set; }

        public bool Deleted { get; set; }

    }

}
