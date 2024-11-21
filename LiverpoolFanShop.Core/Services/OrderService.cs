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
            return await repository.AllReadOnly<Order>()
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    Address = o.Address,
                    CreatedOn = o.OrderDate,
                    ApplicationUserId = o.UserId,
                    ApplicationUser = o.User,
                    OrderProducts = o.OrderProducts.ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderViewModel?> GetOrderByIdAsync(int orderId)
        {
            // Fetch the order including related entities
            var order = await repository.All<Order>()
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderProducts) // Include OrderProducts
                 .ThenInclude(op => op.Product) // Include Product details
                .Include(o => o.User) // Include ApplicationUser
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
