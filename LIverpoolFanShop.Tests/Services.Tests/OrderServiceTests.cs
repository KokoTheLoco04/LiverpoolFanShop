using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Infrastructure.Data;
using LiverpoolFanShop.Infrastructure.Data.Models;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Infrastructure.Data.Common;
using NUnit.Framework;

namespace LiverpoolFanShop.Tests.Services.Tests
{
    public class OrderServiceTests
    {
        private LiverpoolFanShopDbContext _context = null!;
        private Repository _repository = null!;
        private OrderService _orderService = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<LiverpoolFanShopDbContext>()
                .UseInMemoryDatabase(databaseName: "LiverpoolFanShopTestDb")
                .Options;

            _context = new LiverpoolFanShopDbContext(options);
            _repository = new Repository(_context);
            _orderService = new OrderService(_repository);
        }

        [Test]
        public async Task CreateOrderAsync_ShouldCreateOrder_WhenValidDataIsProvided()
        {
            var userId = "user123";
            var address = "123 Test St, Test City";
            var products = new List<ProductInShoppingCartViewModel>
            {
                new ProductInShoppingCartViewModel
                {
                    ProductId = 1,
                    ProductName = "Test Product",
                    Amount = 2,
                    Price = 10.00m,
                    ImageUrl = "http://example.com/product.jpg"
                },
                new ProductInShoppingCartViewModel
                {
                    ProductId = 2,
                    ProductName = "Another Product",
                    Amount = 1,
                    Price = 20.00m,
                    ImageUrl = "http://example.com/product2.jpg"
                }
            };

            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            var orderId = await _orderService.CreateOrderAsync(userId, address, products);

            var order = await _context.Orders.FindAsync(orderId);
            Assert.That(order, Is.Not.Null); 
            Assert.That(order.UserId, Is.EqualTo(userId)); 
            Assert.That(order.Address, Is.EqualTo(address));
            Assert.That(order.TotalAmount, Is.EqualTo(2 * 10.00m + 1 * 20.00m)); 

            var orderProducts = await _context.OrderProducts.Where(op => op.OrderId == orderId).ToListAsync();
            Assert.That(orderProducts.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateOrderAsync_ShouldThrowArgumentException_WhenProductsAreNullOrEmpty()
        {
            var userId = "user123";
            var address = "123 Test St, Test City";
            var products = new List<ProductInShoppingCartViewModel>();

            var ex = Assert.ThrowsAsync<ArgumentException>(() =>
                _orderService.CreateOrderAsync(userId, address, products));

            Assert.That(ex.Message, Is.EqualTo("Order must contain at least one product."));
        }

        [Test]
        public async Task CreateOrderAsync_ShouldCorrectlyCalculateTotalAmount()
        {
            var userId = "user123";
            var address = "123 Test St, Test City";
            var products = new List<ProductInShoppingCartViewModel>
            {
                new ProductInShoppingCartViewModel
                {
                    ProductId = 1,
                    ProductName = "Test Product",
                    Amount = 2,
                    Price = 10.00m,
                    ImageUrl = "http://example.com/product.jpg"
                },
                new ProductInShoppingCartViewModel
                {
                    ProductId = 2,
                    ProductName = "Another Product",
                    Amount = 1,
                    Price = 20.00m,
                    ImageUrl = "http://example.com/product2.jpg"
                }
            };

            var expectedTotalAmount = 2 * 10.00m + 1 * 20.00m;

            var orderId = await _orderService.CreateOrderAsync(userId, address, products);

            var order = await _context.Orders.FindAsync(orderId);
            Assert.That(order?.TotalAmount, Is.EqualTo(expectedTotalAmount));
        }

        [Test]
        public async Task GetOrdersForUserByIdAsync_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            var userId = "user123";

            var result = await _orderService.GetOrdersForUserByIdAsync(userId);

            Assert.That(result, Is.Empty);
        }


        [Test]
        public async Task GetOrdersForUserByIdAsync_ShouldReturnOrders_WhenOrdersExistForUser()
        {
            var userId = "user123";
            var address = "123 Test St, Test City";

            var product1 = new Product
            {
                Id = 1,
                Name = "Test Product 1",
                Description = "Description of Product 1"
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Test Product 2",
                Description = "Description of Product 2"
            };

            var order = new Order
            {
                Id = 1,
                UserId = userId,
                Address = address,
                OrderDate = DateTime.UtcNow,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { ProductId = product1.Id, Quantity = 2, Price = 10.00m, Product = product1 },
                    new OrderProduct { ProductId = product2.Id, Quantity = 1, Price = 20.00m, Product = product2 }
                }
            };

            _context.ChangeTracker.Clear();

            _context.Add(product1);
            _context.Add(product2);
            _context.Add(order);
            await _context.SaveChangesAsync();


            var result = await _orderService.GetOrdersForUserByIdAsync(userId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(1));

            var orderViewModel = result.First();
            Assert.That(orderViewModel.Address, Is.EqualTo(address));
            Assert.That(orderViewModel.ApplicationUserId, Is.EqualTo(userId));

            Assert.That(orderViewModel.OrderProducts.Count, Is.EqualTo(2));
            Assert.That(orderViewModel.OrderProducts[0].Product.Name, Is.EqualTo("Test Product 1"));
            Assert.That(orderViewModel.OrderProducts[1].Product.Name, Is.EqualTo("Test Product 2"));
        }


        [Test]
        public async Task GetOrdersForUserByIdAsync_ShouldReturnCorrectOrderData()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var userId = "user123";
            var address = "123 Test St, Test City";

            var product1 = new Product
            {
                Name = "Test Product 1",
                Description = "Description of Product 1",
                Price = 10.00m,
                AmountInStock = 10,
                CategoryId = 1
            };

            var product2 = new Product
            {
                Name = "Test Product 2",
                Description = "Description of Product 2",
                Price = 20.00m,
                AmountInStock = 5,
                CategoryId = 1
            };

            var order = new Order
            {
                UserId = userId,
                Address = address,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 40.00m,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { Product = product1, Quantity = 2, Price = 10.00m },
                    new OrderProduct { Product = product2, Quantity = 1, Price = 20.00m }
                }
            };

            _context.Products.AddRange(product1, product2);
            _context.Orders.Add(order);
            _context.SaveChanges();

            var result = await _orderService.GetOrdersForUserByIdAsync(userId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));

            var orderViewModel = result.First();

            decimal expectedTotalAmount = (2 * 10.00m) + (1 * 20.00m);
            Assert.That(orderViewModel.TotalAmount, Is.EqualTo(expectedTotalAmount));
        }

        [Test]
        public async Task GetOrderByIdAsync_ShouldReturnOrderViewModel_WhenOrderExists()
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
            await _context.Products.AddAsync(product);

            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com"
            };
            await _context.Users.AddAsync(user);

            var order = new Order
            {
                Id = 1,
                UserId = userId,
                Address = "123 Test St.",
                OrderDate = DateTime.UtcNow,
                User = user,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { ProductId = product.Id, Product = product, Quantity = 2, Price = 50.00m }
                }
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var result = await _orderService.GetOrderByIdAsync(order.Id);

            Assert.That(result, Is.Not.Null); 
            Assert.That(result.Id, Is.EqualTo(order.Id)); 
            Assert.That(result.ApplicationUserId, Is.EqualTo(userId)); 
            Assert.That(result.OrderProducts, Has.Exactly(1).Items); 
            Assert.That(result.OrderProducts.First().Product.Name, Is.EqualTo("Test Product"));
            Assert.That(result.TotalAmount, Is.EqualTo(100.00m)); 
        }


        [Test]
        public async Task GetOrderByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            var orderId = 999; 

            var result = await _orderService.GetOrderByIdAsync(orderId);

            Assert.That(result, Is.Null, "The result should be null when the order does not exist.");
        }

        [Test]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            var userId = "user123";

            var category = new Category { Id = 1, Name = "TestCategory" };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            var product1 = new Product
            {
                Id = 1,
                Name = "Test Product 1",
                Price = 50.00m,
                AmountInStock = 100,
                ImageUrl = "https://example.com/product1.jpg",
                CategoryId = category.Id,
                Category = category
            };
            var product2 = new Product
            {
                Id = 2,
                Name = "Test Product 2",
                Price = 75.00m,
                AmountInStock = 50,
                ImageUrl = "https://example.com/product2.jpg",
                CategoryId = category.Id,
                Category = category
            };
            await _context.Products.AddRangeAsync(product1, product2);

            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com"
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var order1 = new Order
            {
                Id = 1,
                UserId = userId,
                Address = "123 Test St.",
                OrderDate = DateTime.UtcNow,
                User = user,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { ProductId = product1.Id, Product = product1, Quantity = 2, Price = 50.00m }
                }
            };
            var order2 = new Order
            {
                Id = 2,
                UserId = userId,
                Address = "456 Test Ave.",
                OrderDate = DateTime.UtcNow,
                User = user,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { ProductId = product2.Id, Product = product2, Quantity = 1, Price = 75.00m }
                }
            };
            await _context.Orders.AddRangeAsync(order1, order2);
            await _context.SaveChangesAsync();

            var result = await _orderService.GetAllOrdersAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));

            var orderViewModels = result.OrderBy(o => o.Id).ToList();
            var orderViewModel1 = orderViewModels[0];
            var orderViewModel2 = orderViewModels[1];

            Assert.Multiple(() =>
            {
                Assert.That(orderViewModel1.Id, Is.EqualTo(order1.Id));
                Assert.That(orderViewModel1.ApplicationUserId, Is.EqualTo(userId));
                Assert.That(orderViewModel1.OrderProducts, Has.Exactly(1).Items);
                Assert.That(orderViewModel1.OrderProducts.First().Product.Name, Is.EqualTo("Test Product 1"));
                Assert.That(orderViewModel1.TotalAmount, Is.EqualTo(100.00m).Within(0.01m));

                Assert.That(orderViewModel2.Id, Is.EqualTo(order2.Id));
                Assert.That(orderViewModel2.ApplicationUserId, Is.EqualTo(userId));
                Assert.That(orderViewModel2.OrderProducts, Has.Exactly(1).Items);
                Assert.That(orderViewModel2.OrderProducts.First().Product.Name, Is.EqualTo("Test Product 2"));
                Assert.That(orderViewModel2.TotalAmount, Is.EqualTo(75.00m).Within(0.01m));
            });
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
