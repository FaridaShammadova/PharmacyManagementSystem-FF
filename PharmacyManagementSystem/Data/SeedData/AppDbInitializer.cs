using Microsoft.AspNetCore.Identity;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Data.SeedData
{
    public static class AppDbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // ROLES
            string[] roles =
            {
                "Admin",
                "Seller",
                "Customer"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // DEFAULT ADMIN
            string adminEmail = "admin@pharmacy.com";
            string adminPassword = "Admin123!";
            string adminUserName = "Admin";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is null)
            {
                var admin = new ApplicationUser
                {
                    FullName = "System Admin",
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
