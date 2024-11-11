﻿using LiverpoolFanShop.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface ICartService
    {
        Task AddProductToCartAsync(int productId, string userId, int quantity); 
        Task RemoveProductFromCartAsync(int productId, string userId); 
        Task UpdateProductQuantityAsync(int productId, string userId, int quantity); 
        Task<List<ProductInShoppingCartViewModel>> GetCartItemsAsync(string userId); 
        Task ClearCartAsync(string userId); 
        Task<decimal> GetCartTotalAsync(string userId);
    }
}