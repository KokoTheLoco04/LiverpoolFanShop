using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Models.ShoppingCart;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        .Include(sc => sc.ShoppingCartProducts)
        .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (shoppingCart != null && shoppingCart.ShoppingCartProducts.Any())
            {
                await repository.DeleteRangeAsync(shoppingCart.ShoppingCartProducts);
            }
        }

        public async Task CreateCartForUserByIdAsync(string id)
        {
            await this.repository.AddAsync(new ShoppingCart() { UserId = id });
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> DoesUserHasCartAsync(string id)
        {
            return await repository.All<ShoppingCart>().Where(sc => sc.UserId == id).AnyAsync();
        }

        public async Task<ShoppingCartViewModel> GetCartByUserIdAsync(string userId)
        {
            var cart = await repository.AllReadOnly<ShoppingCart>()
            .Where(c => c.UserId == userId)
            .Include(c => c.ShoppingCartProducts)
            .FirstOrDefaultAsync();

            if (cart == null)
            {
                return null;
            }

            var cartViewModel = new ShoppingCartViewModel
            {
                Id = cart.Id,
                ApplicationUserId = cart.UserId,
                ShoppingCartProducts = cart.ShoppingCartProducts.Select(item => new ProductInShoppingCartViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Amount = item.Quantity,
                    ImageUrl = item.Product.ImageUrl
                }).ToList()
            };

            return cartViewModel;
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
            var shoppingCart = await repository.All<ShoppingCart>()
                                               .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (shoppingCart == null)
            {
                throw new InvalidOperationException("Shopping cart not found.");
            }

            var cartProduct = await repository.All<ShoppingCartProduct>()
                                              .FirstOrDefaultAsync(cp => cp.ProductId == productId && cp.ShoppingCartId == shoppingCart.Id);

            if (cartProduct == null)
            {
                throw new InvalidOperationException("The product does not exist in the cart.");
            }

            await repository.DeleteAsync<ShoppingCartProduct>(productId, shoppingCart.Id);
            await repository.SaveChangesAsync();
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
