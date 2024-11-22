using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiverpoolFanShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public OrderController(ICartService _cartService, IOrderService _orderService, IProductService _productService)
        {
            cartService = _cartService;
            orderService = _orderService;
            productService = _productService;
        }

        [HttpPost]
        public async Task<IActionResult> FinishOrder(MakeOrderInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var cartItems = await cartService.GetCartItemsAsync(userId);

                if (!cartItems.Any())
                {
                    ModelState.AddModelError("", "Your cart is empty.");
                    return View(model);
                }

                foreach (var item in cartItems)
                {
                    await productService.DecreaseProductAmountAsync(item.ProductId, item.Amount);
                }

                var orderId = await orderService.CreateOrderAsync(userId, model.Address, cartItems);

                await cartService.ClearCartAsync(userId);

                return RedirectToAction("OrderConfirmation", "Order", new { orderId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await orderService.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                TempData["Error"] = "Order not found.";
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var userOrders = await orderService.GetOrdersForUserByIdAsync(userId);

            return View(userOrders);
        }
    }
}
