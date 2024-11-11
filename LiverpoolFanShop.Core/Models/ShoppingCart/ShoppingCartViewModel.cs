using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public IEnumerable<ProductInShoppingCartViewModel> ShoppingCartProducts { get; set; } = new HashSet<ProductInShoppingCartViewModel>();
    }
}
