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

        //[HttpGet]
        //public async Task<IActionResult> Checkout()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var cart = await cartService.GetCartByUserIdAsync(userId);
        //    if (cart == null || !cart.ShoppingCartProducts.Any())
        //    {
        //        TempData["Error"] = "Your cart is empty.";
        //        return RedirectToAction("Cart", "Carts");
        //    }

        //    var viewModel = new MakeOrderInputViewModel
        //    {
        //        ShoppingCartId = cart.Id.ToString(),
        //        ShoppingCartProducts = cart.ShoppingCartProducts.Select(item => new ProductInShoppingCartViewModel
        //        {
        //            ProductId = item.ProductId,
        //            ProductName = item.ProductName,
        //            Price = item.Price,
        //            Amount = item.Amount,
        //            ImageUrl = item.ImageUrl
        //        }).ToList()
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Checkout(MakeOrderInputViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Create the order (save to database)
        //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get user info
        //        var order = await orderService.CreateOrderAsync(model, userId);

        //        if (order != null)
        //        {
        //            // Decrease stock for each product in the order.
        //            foreach (var product in model.Products)
        //            {
        //                await productService.DecreaseProductAmountAsync(product.Id, product.Quantity);
        //            }

        //            // Redirect to the order confirmation page.
        //            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        //        }

        //        TempData["Error"] = "There was an error completing your order.";
        //        return RedirectToAction("Checkout");
        //    }

        //    // If the form is not valid, return to checkout with validation errors.
        //    TempData["Error"] = "Invalid order data.";
        //    return RedirectToAction("Checkout");
        //}


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
    }
}
