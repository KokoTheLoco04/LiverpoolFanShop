using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Category;
using LiverpoolFanShop.Core.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService _productService)
        {
            productService = _productService;
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
                ImageUrl = product.ImageUrl, // Make sure ImageUrl is assigned here
                AmountInStock = product.AmountInStock,
                Category = new ProductCategoryModel 
                { 
                    Id = product.Id, 
                    Name = product.Name 
                }
            };

            return View(viewModel);
        }
    }
}
