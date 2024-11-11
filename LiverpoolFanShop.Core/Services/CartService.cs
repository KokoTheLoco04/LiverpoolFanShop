using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class CartService : ICartService
    {
        public Task AddProductToCartAsync(int productId, string userId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductInShoppingCartViewModel>> GetCartItemsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetCartTotalAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductFromCartAsync(int productId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductQuantityAsync(int productId, string userId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
