﻿using BeTech.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class Product
    {
        public int ProductId{ get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal PriceInBaseCurrency { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool Deleted { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }

        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [Required]
        public string BarcodeValue { get; set; }

        [Required]
        public byte[] Barcode { get; set; }

        public List<StockProduct> StockProduct { get; set; }

    }
}
