using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LiverpoolFanShop.Core.Models.Product
{
    public class ProductInShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Amount { get; set; } 
        public decimal Price { get; set; } 
        public decimal TotalPrice { get { return Price * Amount; } }
        public string ImageUrl { get; set; } = string.Empty;

    }
}
