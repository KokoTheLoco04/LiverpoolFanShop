using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
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
        public async Task AddProductToOrderAsync(int productId, int amount, int orderId)
        {
            var order = await repository.GetByIdAsync<Order>(orderId);
            
            if (order == null) 
            { 
                throw new ArgumentException($"Order with ID {orderId} does not exist."); 
            }
            
            var product = await repository.GetByIdAsync<Product>(productId); 
            
            if (product == null) 
            { 
                throw new ArgumentException($"Product with ID {productId} does not exist."); 
            }
            
            var orderProduct = new OrderProduct 
            { 
                OrderId = orderId, 
                ProductId = productId, 
                Quantity = amount, 
                Price = product.Price * amount 
            }; 
            
            await repository.AddAsync(orderProduct); 
            await repository.SaveChangesAsync();
        }

        public async Task<int> CreateOrderAsync(string userId, string address)
        {
            var orderToAdd = new Order
            {
                UserId = userId,
                Address = address,
                OrderDate = DateTime.Now
            };

            await repository.AddAsync(orderToAdd);
            await repository.SaveChangesAsync();

            return orderToAdd.Id;
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
    }
}
