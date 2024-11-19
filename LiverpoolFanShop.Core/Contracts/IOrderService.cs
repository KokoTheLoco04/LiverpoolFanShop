using LiverpoolFanShop.Core.Models.Order;
using LiverpoolFanShop.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiverpoolFanShop.Core.Contracts
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(string userId, string address, List<ProductInShoppingCartViewModel> products);

        Task<List<OrderViewModel>> GetOrdersForUserByIdAsync(string userId);
    }
}
