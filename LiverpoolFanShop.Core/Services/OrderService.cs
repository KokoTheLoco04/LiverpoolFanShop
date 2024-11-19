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

            var user = await repository.GetByIdAsync<ApplicationUser>(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }

            var orderToAdd = new Order
            {
                UserId = userId,
                User = user,
                Address = address,
                OrderDate = DateTime.Now,
                OrderProducts = products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Amount,
                    Price = p.Price * p.Amount
                }).ToList()
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
