using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Order;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Models.ShoppingCart;
using LiverpoolFanShop.Core.Services;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiverpoolFanShop.Controllers
{
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(ICartService _cartService, IProductService _productService, IOrderService _orderService, UserManager<ApplicationUser> _userManager)
        {
            cartService = _cartService;
            productService = _productService;
            orderService = _orderService;
            userManager = _userManager;
        }

        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var cartItems = await cartService.GetCartItemsAsync(userId);
            var cartTotal = await cartService.GetCartTotalAsync(userId);

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Info"] = "Your cart is empty.";
                return View(new ShoppingCartViewModel
                {
                    ShoppingCartProducts = new List<ProductInShoppingCartViewModel>(),
                    ApplicationUserId = userId,
                    Id = 0
                });
            }

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartProducts = cartItems,
                Id = 0, // Update with actual cart ID if applicable
                ApplicationUserId = userId
            };

            ViewBag.CartTotal = cartTotal;

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(ProductAddToCartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid product details. Please try again.";
                return RedirectToAction("Details", "Products", new { id = model.ProductId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var product = await productService.GetProductByIdAsync(model.ProductId);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction("Index", "Home");
            }

            if (model.Amount > product.AmountInStock)
            {
                TempData["Error"] = $"Only {product.AmountInStock} items are available in stock.";
                return RedirectToAction("Details", "Products", new { id = model.ProductId });
            }

            await cartService.AddProductToCartAsync(model.ProductId, userId, model.Amount);

            TempData["Success"] = "Product successfully added to your cart!";
            return RedirectToAction("Details", "Products", new { id = model.ProductId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var hasCart = await cartService.DoesUserHasCartAsync(userId);
            if (!hasCart)
            {
                await cartService.CreateCartForUserByIdAsync(userId);
                TempData["Error"] = "No cart found. A new cart has been created.";
                return RedirectToAction("Cart");
            }

            try
            {
                await cartService.RemoveProductFromCartAsync(productId, userId);
                TempData["Success"] = "Product removed from cart.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var hasCart = await cartService.DoesUserHasCartAsync(userId);
            if (!hasCart)
            {
                await cartService.CreateCartForUserByIdAsync(userId);
                TempData["Error"] = "No cart found. A new cart has been created.";
                return RedirectToAction("Cart");
            }

            try
            {
                await cartService.UpdateProductQuantityAsync(productId, userId, quantity);
                TempData["Success"] = "Product quantity updated.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating quantity: {ex.Message}";
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var hasCart = await cartService.DoesUserHasCartAsync(userId);
            if (!hasCart)
            {
                await cartService.CreateCartForUserByIdAsync(userId);
                TempData["Error"] = "No cart found. A new cart has been created.";
                return RedirectToAction("Cart");
            }

            try
            {
                await cartService.ClearCartAsync(userId);
                TempData["Success"] = "Cart cleared.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error clearing the cart: {ex.Message}";
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var cartItems = await cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Home");
            }

            var totalAmount = cartItems.Sum(item => item.Price * item.Amount);

            var checkoutViewModel = new MakeOrderInputViewModel
            {
                ShoppingCartId = userId,
                Address = string.Empty
            };

            ViewBag.CartTotal = totalAmount;

            return View(checkoutViewModel);
        }
    }
}

