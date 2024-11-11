using LiverpoolFanShop.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(string userId, string address);

        Task AddProductToOrderAsync(int productId, int amount, string orderId);

        Task<List<OrderViewModel>> GetOrdersForUserByIdAsync(string userId);
    }
}
