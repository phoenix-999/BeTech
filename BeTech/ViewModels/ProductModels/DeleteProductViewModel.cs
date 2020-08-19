using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BeTech.ViewModels.ProductModels
{
    public class DeleteProductViewModel
    {
        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
