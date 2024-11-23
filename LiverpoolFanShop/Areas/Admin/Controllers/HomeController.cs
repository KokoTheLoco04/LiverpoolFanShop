using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
