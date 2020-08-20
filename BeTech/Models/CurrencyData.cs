using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BeTech.Models
{
    public class CurrencyData
    {
        public int r030 { get; set; }
        public string txt { get; set; }
        public string rate { get; set; }
        public string cc { get; set; }
        public string exchangedate { get; set; }
    }
}
