using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels
{
    public class DeleteProductCategoryViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
