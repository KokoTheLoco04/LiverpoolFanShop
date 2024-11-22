using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Core.Models.Product;
using LiverpoolFanShop.Infrastructure.Data.Common;
using LiverpoolFanShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : AdminBaseController
    {
        private readonly IProductCategoryService categoryService; // Service to manage categories
        private readonly IProductService productService; // Service to manage products
        private readonly IRepository repository;

        public ProductsController(IProductCategoryService categoryService, IProductService productService, IRepository repository)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.repository = repository;
        }

        // Display the add product form
        public async Task<IActionResult> Add()
        {
            var model = new ProductFormModel
            {
                Categories = await categoryService.AllCategoriesAsync() // Fetch categories to populate dropdown
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }

            if (await productService.DoesProductExistByNameAsync(model.Name))
            {
                ModelState.AddModelError(string.Empty, "A product with this name already exists.");
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                AmountInStock = model.AmountInStock,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Home", new { area = "Admin" });
        }

        public IActionResult Edit() 
        {
            return View();
        }
    }
}
