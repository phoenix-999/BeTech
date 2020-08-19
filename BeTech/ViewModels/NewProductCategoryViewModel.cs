using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels
{
    public class NewProductCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
