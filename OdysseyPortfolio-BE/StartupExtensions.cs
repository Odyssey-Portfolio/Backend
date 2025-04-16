using Microsoft.AspNetCore.Identity;
using OdysseyPortfolio_Libraries.Constants;
using OdysseyPortfolio_Libraries.Entities;
using OdysseyPortfolio_Libraries.Helpers;
using System.Reflection;

namespace OdysseyPortfolio_BE
{
    public static class StartupExtensions
    {
        public static async Task InitializeSecurityAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await RoleUtils.SeedRolesAsync(roleManager);
        }

        public static async Task SeedRootAdminAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var email = config["RootAdmin:Email"];
            var password = config["RootAdmin:Password"];
            var name = config["RootAdmin:Name"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new Exception("RootAdmin credentials not configured properly.");

            // Ensure roles exist
            var roles = typeof(UserRoles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetValue(null)?.ToString())
                .Where(r => r != null);

            foreach (var role in roles!)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create root admin if it doesn't exist
            var existing = await userManager.FindByEmailAsync(email);
            if (existing == null)
            {
                var user = new User { UserName = email, Email = email, Name = name };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                else
                    throw new Exception("Failed to create root admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
