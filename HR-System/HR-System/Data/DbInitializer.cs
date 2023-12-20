using HR_System.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HR_System.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<AuthUser>>();

            await SeedRolesAsync(services);
            await SeedAdminUserAsync(userManager);
        }

        private static async Task SeedRolesAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "HR", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<AuthUser> userManager)
        {
            var adminUser = await userManager.FindByNameAsync("Admin");

            if (adminUser == null)
            {
                adminUser = new AuthUser
                {
                    UserName = "Admin",
                    Email = "admin@a.com",
                };

                await userManager.CreateAsync(adminUser, "Admin!23");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

}
