using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Services
{
    public class OrderService : IOrderService
    {
        public Task AddProductToOrderAsync(int productId, int amount, string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateOrderAsync(string userId, string address)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOrdersForUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
