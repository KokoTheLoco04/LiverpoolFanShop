using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Models.Product
{
    public class ProductAddToCartViewModel
    {
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public int Amount { get; set; }


    }
}
