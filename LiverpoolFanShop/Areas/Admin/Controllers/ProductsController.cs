using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : AdminBaseController
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit() 
        {
            return View();
        }
    }
}
