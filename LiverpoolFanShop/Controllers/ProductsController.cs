﻿using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Category;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace LiverpoolFanShop.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public ProductsController(IProductService _productService, ICartService _cartService)
        {
            productService = _productService;
            cartService = _cartService;
        }
        public async Task<IActionResult> ProductsByCategory(int id)
        {
            var products = await productService.GetProductsByCategoryAsync(id);
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                AmountInStock = product.AmountInStock,
                Category = new ProductCategoryModel 
                { 
                    Id = product.Category.Id, 
                    Name = product.Name 
                }
            };

            return View(viewModel);
        }
    }
}
