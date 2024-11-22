using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
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
    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<int> CreateOrderAsync(string userId, string address, List<ProductInShoppingCartViewModel> products)
        {
            if (products == null || !products.Any())
            {
                throw new ArgumentException("Order must contain at least one product.");
            }

            decimal totalAmount = products.Sum(p => p.TotalPrice);

            var order = new Order
            {
                UserId = userId,
                Address = address,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount, 
                OrderProducts = products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Amount,
                    Price = p.Price * p.Amount 
                }).ToList()
            };

            await repository.AddAsync(order);
            await repository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<List<OrderViewModel>> GetOrdersForUserByIdAsync(string userId)
        {
            var orders = await repository.All<Order>()
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderProducts) // Load related products
            .ThenInclude(op => op.Product) // Assuming there's a Product entity
            .Select(o => new OrderViewModel
            {
                Id = o.Id,
                Address = o.Address,
                CreatedOn = o.OrderDate,
                ApplicationUserId = o.UserId,
                OrderProducts = o.OrderProducts.Select(op => new OrderProduct
                {
                    ProductId = op.ProductId,
                    Product = new Product
                    {
                        Name = op.Product.Name,
                        Description = op.Product.Description
                    },
                    Price = op.Price,
                    Quantity = op.Quantity
                }).ToList()
            })
            .ToListAsync();

            return orders;
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(int orderId)
        {
            var order = await repository.All<Order>()
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderProducts)
                 .ThenInclude(op => op.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                Id = order.Id,
                Address = order.Address,
                CreatedOn = order.OrderDate,
                ApplicationUserId = order.UserId,
                ApplicationUser = order.User,
                OrderProducts = order.OrderProducts.Select(op => new OrderProduct
                {
                    ProductId = op.ProductId,
                    Product = op.Product,
                    Quantity = op.Quantity,
                    Price = op.Price
                }).ToList()
            };
        }
    }
}
