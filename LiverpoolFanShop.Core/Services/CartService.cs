using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repository;

        public CartService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddProductToCartAsync(int productId, string userId, int quantity)
        {
            var shoppingCart = await repository.All<ShoppingCart>()
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart {UserId = userId };
                await repository.AddAsync(shoppingCart);
                await repository.SaveChangesAsync();
            }

            var product = await repository.GetByIdAsync<Product>(productId);

            if(product != null)
            {
                var cartProduct = new ShoppingCartProduct
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ShoppingCartId = shoppingCart.Id
                };

                shoppingCart.ShoppingCartProducts.Add(cartProduct);
                await repository.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var shoppingCart = await repository.All<ShoppingCart>()
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if(shoppingCart != null)
            {
                shoppingCart.ShoppingCartProducts.Clear();
                await repository.SaveChangesAsync();
            }
        }

        public async Task<List<ProductInShoppingCartViewModel>> GetCartItemsAsync(string userId)
        {
            var cartProducts = await repository.All<ShoppingCartProduct>()
                .Where(scp => scp.ShoppingCart.UserId == userId)
                .Include(scp => scp.Product)
                .ToListAsync();

            return cartProducts.Select(cp => new ProductInShoppingCartViewModel
            {
                ProductId = cp.ProductId,
                ProductName = cp.Product.Name,
                Amount = cp.Quantity,
                Price = cp.Product.Price,
                ImageUrl = cp.Product.ImageUrl,
            }).ToList();
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            var cartProducts = await repository.All<ShoppingCartProduct>()
                .Where(ci => ci.ShoppingCart.UserId == userId)
                .Include(ci => ci.Product)
                .ToListAsync(); 
            
            return (decimal)cartProducts.Sum(ci => ci.Product.Price * ci.Quantity);
        }

        public async Task RemoveProductFromCartAsync(int productId, string userId)
        {
            var cartProduct = await repository.All<ShoppingCartProduct>()
                .FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.ShoppingCart.UserId == userId);

            if (cartProduct != null)
            {
                await repository.DeleteAsync<ShoppingCartProduct>(cartProduct);
                await repository.SaveChangesAsync();
            }
        }

        public async Task UpdateProductQuantityAsync(int productId, string userId, int quantity)
        {
            var cartProduct = await repository.All<ShoppingCartProduct>()
                .FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.ShoppingCart.UserId == userId);

            if (cartProduct != null)
            {
                cartProduct.Quantity = quantity;
                await repository.SaveChangesAsync();
            }
        }
    }
}
