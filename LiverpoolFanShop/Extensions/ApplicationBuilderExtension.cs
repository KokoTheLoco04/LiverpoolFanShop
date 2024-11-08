using LiverpoolFanShop.Infrastructure.Data.Models;
using static LiverpoolFanShop.Core.Constants.AdministratorConstants;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtension
    {
        public static async Task CreateAdminRoleAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager != null && await roleManager.RoleExistsAsync(AdminRole) == false)
            {
                var role = new IdentityRole(AdminRole);
                await roleManager.CreateAsync(role);
            }

            if (userManager != null)
            {
                var admin = await userManager.FindByEmailAsync(AdminEmail);

                if (admin != null && await userManager.IsInRoleAsync(admin, AdminRole) == false)
                {
                    await userManager.AddToRoleAsync(admin, AdminRole);
                }
            }

        }

    }
}
