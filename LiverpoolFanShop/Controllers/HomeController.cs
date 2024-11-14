using LiverpoolFanShop.Core.Contracts;
using LiverpoolFanShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LiverpoolFanShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductCategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, IProductCategoryService _categoryService)
        {
            _logger = logger;
            categoryService = _categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.AllCategoriesAsync();
            return View(categories);
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
