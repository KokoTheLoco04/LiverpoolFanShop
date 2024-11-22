using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Enumerations;
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
        private readonly IProductCategoryService categoryService;
        private readonly ICartService cartService;

        public ProductsController(IProductService _productService, IProductCategoryService _categoryService, ICartService _cartService)
        {
            productService = _productService;
            categoryService = _categoryService;
            cartService = _cartService;
        }
        public async Task<IActionResult> ProductsByCategory(int id, int currentPage = 1, int productsPerPage = 3, string searchTerm = "", ProductSorting sorting = ProductSorting.Default)
        {
            var queryModel = new AllProductsQueryModel
            {
                CategoryId = id,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                SearchTerm = searchTerm,
                Sorting = sorting
            };

            var result = await productService.GetAllProductsAsync(queryModel);

            queryModel.TotalProductsCount = result.TotalProducts;
            queryModel.Products = result.Products;

            return View(queryModel);
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
