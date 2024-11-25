using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using LiverpoolFanShop.Core.Models.ShoppingCart;
using LiverpoolFanShop.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiverpoolFanShop.Infrastructure.Data;

namespace LiverpoolFanShop.Tests.Services.Tests
{

    [TestFixture]
    public class CartServiceTests
    {
        private LiverpoolFanShopDbContext _dbContext = null!;
        private Repository _repository = null!;
        private CartService _cartService = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LiverpoolFanShopDbContext>()
                .UseInMemoryDatabase(databaseName: "LiverpoolFanShopTestDb")
                .Options;

            _dbContext = new LiverpoolFanShopDbContext(options);
            _repository = new Repository(_dbContext);
            _cartService = new CartService(_repository);
        }

        [Test]
        public async Task AddProductToCartAsync_ShouldAddProductToCart()
        {
            var userId = "user123";
            var product = new Product { Id = 1, Name = "Scarf", Price = 19.99M };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            await _cartService.AddProductToCartAsync(product.Id, userId, 2);

            var cart = await _dbContext.ShoppingCarts.Include(sc => sc.ShoppingCartProducts)
                                                      .FirstOrDefaultAsync(sc => sc.UserId == userId);

            Assert.That(cart, Is.Not.Null);
            Assert.That(cart?.ShoppingCartProducts.Count, Is.EqualTo(1));

            var cartProduct = cart?.ShoppingCartProducts.First();
            Assert.That(cartProduct?.ProductId, Is.EqualTo(product.Id));
            Assert.That(cartProduct?.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task ClearCartAsync_ShouldRemoveAllProductsFromCart()
        {
            var userId = "user123";
            var shoppingCart = new ShoppingCart { UserId = userId };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct { ProductId = 1, Quantity = 1 });
            shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct { ProductId = 2, Quantity = 2 });
            await _dbContext.SaveChangesAsync();

            await _cartService.ClearCartAsync(userId);

            var cart = await _dbContext.ShoppingCarts.Include(sc => sc.ShoppingCartProducts)
                                                      .FirstOrDefaultAsync(sc => sc.UserId == userId);

            Assert.That(cart?.ShoppingCartProducts, Is.Empty);
        }

        [Test]
        public async Task CreateCartForUserByIdAsync_ShouldCreateShoppingCart()
        {
            var userId = "user123";

            await _cartService.CreateCartForUserByIdAsync(userId);
            var result = await _dbContext.ShoppingCarts.FirstOrDefaultAsync(sc => sc.UserId == userId);

            Assert.That(result, Is.Not.Null, "Shopping cart should be created.");
            Assert.That(result?.UserId, Is.EqualTo(userId), "UserId should match the created cart.");
        }

        [Test]
        public async Task DoesUserHasCartAsync_ShouldReturnTrue_WhenCartExists()
        {
            var userId = "user123";
            var shoppingCart = new ShoppingCart { UserId = userId };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            var result = await _cartService.DoesUserHasCartAsync(userId);

            Assert.That(result, Is.True, "Should return true when the user has a cart.");
        }

        [Test]
        public async Task DoesUserHasCartAsync_ShouldReturnFalse_WhenCartDoesNotExist()
        {
            var userId = "user123";

            var result = await _cartService.DoesUserHasCartAsync(userId);

            Assert.That(result, Is.False, "Should return false when the user does not have a cart.");
        }


        [Test]
        public async Task GetCartTotalAsync_ShouldReturnTotalPriceOfCart()
        {
            var userId = "user123";
            var shoppingCart = new ShoppingCart { UserId = userId };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct
            {
                Product = new Product { Name = "Scarf", Price = 20M },
                Quantity = 2
            });

            shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProduct
            {
                Product = new Product { Name = "Jersey", Price = 50M },
                Quantity = 1
            });

            await _dbContext.SaveChangesAsync();

            var total = await _cartService.GetCartTotalAsync(userId);

            Assert.That(total, Is.EqualTo(90M));
        }

        [Test]
        public async Task GetCartItemsAsync_ShouldReturnCorrectCartItems()
        {
            var userId = "user123";

            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 50.00m,
                AmountInStock = 100,
                ImageUrl = "https://example.com/product.jpg",
                Category = new Category { Id = 1, Name = "TestCategory" }
            };
            await _dbContext.Products.AddAsync(product);

            var shoppingCart = new ShoppingCart
            {
                UserId = userId
            };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ShoppingCart = shoppingCart,
                Product = product,
                Quantity = 2
            };
            await _dbContext.ShoppingCartProducts.AddAsync(shoppingCartProduct);

            await _dbContext.SaveChangesAsync();

            var result = await _cartService.GetCartItemsAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(1).Items);
            Assert.That(result.First().ProductName, Is.EqualTo("Test Product"));
            Assert.That(result.First().Amount, Is.EqualTo(2));
            Assert.That(result.First().Price, Is.EqualTo(50.00m));
            Assert.That(result.First().ImageUrl, Is.EqualTo("https://example.com/product.jpg"));
        }


        [Test]
        public async Task GetCartByUserIdAsync_ShouldReturnShoppingCartViewModel()
        {
            var userId = "user123";

            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Price = 50.00m,
                AmountInStock = 100,
                ImageUrl = "https://example.com/product.jpg",
                Category = new Category { Id = 1, Name = "TestCategory" }
            };
            await _dbContext.Products.AddAsync(product);

            var shoppingCart = new ShoppingCart
            {
                UserId = userId
            };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);

            var customer = new Customer
            {
                UserId = userId,
                ShoppingCart = shoppingCart
            };
            await _dbContext.Customers.AddAsync(customer);

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ShoppingCart = shoppingCart,
                Product = product,
                Quantity = 2
            };
            await _dbContext.ShoppingCartProducts.AddAsync(shoppingCartProduct);

            await _dbContext.SaveChangesAsync();

            var savedCart = await _dbContext.ShoppingCarts
                .Include(sc => sc.ShoppingCartProducts)
                .ThenInclude(scp => scp.Product)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            Assert.That(savedCart, Is.Not.Null, "Shopping cart should not be null.");
            Assert.That(savedCart?.ShoppingCartProducts, Is.Not.Empty, "Shopping cart should contain products.");
            Assert.That(savedCart?.ShoppingCartProducts.First().Product, Is.Not.Null, "Product should not be null in the cart.");

            var result = await _cartService.GetCartByUserIdAsync(userId);

            Assert.That(result, Is.Not.Null); 
            Assert.That(result.ApplicationUserId, Is.EqualTo(userId));
            Assert.That(result.ShoppingCartProducts, Has.Exactly(1).Items);
            Assert.That(result.ShoppingCartProducts.First().ProductName, Is.EqualTo("Test Product"));
            Assert.That(result.ShoppingCartProducts.First().Amount, Is.EqualTo(2));
        }

        [Test]
        public async Task RemoveProductFromCartAsync_ShouldRemoveProduct()
        {
            var userId = "user123";
            var shoppingCart = new ShoppingCart { UserId = userId };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            var cartProduct = new ShoppingCartProduct
            {
                ProductId = 1,
                Quantity = 2,
                ShoppingCartId = shoppingCart.Id
            };

            shoppingCart.ShoppingCartProducts.Add(cartProduct);
            await _dbContext.SaveChangesAsync();

            await _cartService.RemoveProductFromCartAsync(1, userId);

            var cart = await _dbContext.ShoppingCarts.Include(sc => sc.ShoppingCartProducts)
                                                      .FirstOrDefaultAsync(sc => sc.UserId == userId);

            Assert.That(cart?.ShoppingCartProducts, Is.Empty);
        }

        [Test]
        public async Task UpdateProductQuantityAsync_ShouldUpdateQuantity()
        {
            var userId = "user123";
            var shoppingCart = new ShoppingCart { UserId = userId };
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            var cartProduct = new ShoppingCartProduct
            {
                ProductId = 1,
                Quantity = 2,
                ShoppingCartId = shoppingCart.Id
            };

            shoppingCart.ShoppingCartProducts.Add(cartProduct);
            await _dbContext.SaveChangesAsync();

            await _cartService.UpdateProductQuantityAsync(1, userId, 5);

            var updatedCartProduct = await _dbContext.ShoppingCartProducts.FirstOrDefaultAsync(cp => cp.ProductId == 1);
            Assert.That(updatedCartProduct?.Quantity, Is.EqualTo(5));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }

}
