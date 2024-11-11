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
        public int Id { get; set; }

        public string ShoppingCartProductId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Amount { get; set; }

    }
}
