using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiverpoolFanShop.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
