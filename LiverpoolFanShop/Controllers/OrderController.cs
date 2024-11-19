using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
using LiverpoolFanShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiverpoolFanShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;

        public OrderController(ICartService _cartService, IOrderService _orderService)
        {
            cartService = _cartService;
            orderService = _orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            // Fetch the user's cart by userId (this will ensure the correct shopping cart is used)
            var cart = await cartService.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.ShoppingCartProducts.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Cart", "Carts"); // Ensure the Cart controller is used
            }

            var viewModel = new MakeOrderInputViewModel
            {
                ShoppingCartId = cart.Id.ToString(), // The correct shopping cart ID
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(MakeOrderInputViewModel model)
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

            var cartItems = await cartService.GetCartItemsAsync(userId);
            if (!cartItems.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(model);
            }

            var orderId = await orderService.CreateOrderAsync(userId, model.Address, cartItems);

            await cartService.ClearCartAsync(userId);

            return RedirectToAction("OrderConfirmation", new { orderId });
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await orderService.GetOrdersForUserByIdAsync(userId)
                                          .ContinueWith(t => t.Result.FirstOrDefault(o => o.Id == orderId));

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
