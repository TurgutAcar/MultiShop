using Microsoft.AspNetCore.Identity;
using MultiShop.IdentityServer.Models;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Seeds
{
    public static class RoleSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task SeedAdminAsync(
    UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "turgut@gmail.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "turgutacar05",
                    Email = adminEmail,
                    Name = "Turgut",
                    Surname = "Acar"
                };

                await userManager.CreateAsync(admin, "11111aA*");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

    }

}
