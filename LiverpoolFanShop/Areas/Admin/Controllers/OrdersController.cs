using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Areas.Admin.Controllers
{
    public class OrdersController : AdminBaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        public async Task<IActionResult> AllOrders()
        {
            var orders = await orderService.GetAllOrdersAsync();
            return View(orders);
        }
    }
}
