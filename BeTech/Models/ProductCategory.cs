using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }

        [Key]
        public int CategoryId { get; set; }
        
        [MaxLength(200)]
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }

        public bool Deleted { get; set; }
    }
}
