using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiverpoolFanShop.Infrastructure.Data
{
    public class LiverpoolFanShopDbContext : IdentityDbContext
    {
        public LiverpoolFanShopDbContext(DbContextOptions<LiverpoolFanShopDbContext> options)
            : base(options)
        {
        }
    }
}
