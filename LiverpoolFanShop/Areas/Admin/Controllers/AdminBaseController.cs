using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LiverpoolFanShop.Core.Constants.AdministratorConstants;

namespace LiverpoolFanShop.Areas.Admin.Controllers
{
    [Authorize(Roles = AdminRole)]
    [Area(AdminAreaName)]
    public class AdminBaseController : Controller
    {
    }
}
