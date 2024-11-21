using LiverpoolFanShop.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LiverpoolFanShop.Infrastructure.Constants.DataConstants;

namespace LiverpoolFanShop.Core.Models.Order
{
    public class MakeOrderInputViewModel
    {
        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string ShoppingCartId { get; set; } = string.Empty;

        public List<ProductInShoppingCartViewModel> ShoppingCartProducts { get; set; } = new List<ProductInShoppingCartViewModel>();

        public decimal TotalAmount => ShoppingCartProducts.Sum(p => p.Price * p.Amount);
    }
}
