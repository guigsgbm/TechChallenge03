using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure;

public static class DbInitializer
{
    public static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        SeedRoles(roleManager);
        SeedAdminUser(userManager, configuration);
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("admin").Result)
        {
            var role = new IdentityRole
            {
                Name = "admin"
            };

            _ = roleManager.CreateAsync(role).Result;
        }

        if (!roleManager.RoleExistsAsync("user").Result)
        {
            var role = new IdentityRole
            {
                Name = "user"
            };

            _ = roleManager.CreateAsync(role).Result;
        }
    }

    private static void SeedAdminUser(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        var username = configuration.GetSection("DefaultAdmin")["Username"];
        var password = configuration.GetSection("DefaultAdmin")["Password"];
        var roleName = "admin";

        if (userManager.FindByNameAsync(roleName).Result == null)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = username
            };

            var result = userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                _ = userManager.AddToRoleAsync(user, roleName).Result;
            }
        }
    }
}