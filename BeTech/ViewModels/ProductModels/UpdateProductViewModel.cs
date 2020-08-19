using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BeTech.Models;

namespace BeTech.ViewModels.ProductModels
{
    public class UpdateProductViewModel : NewProductViewModel
    {
        [Required]
        public int ProductId { get; set; }

        public Currency CurrentCurrency { get; set; }
        public ProductCategory CurrentCategory { get; set; }
    }
}
