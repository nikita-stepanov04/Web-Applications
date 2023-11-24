using Microsoft.AspNetCore.Identity;
using RestaurantApi.Models.IdentityContext;

namespace RestaurantApi.Models.DataContext
{
    public static class SeedIdentity
    {
        public static async void SeedDatabase(IApplicationBuilder app, IConfiguration config)
        {
            string? adminEmail = config["AdminSettings:AdminEmail"];
            string? adminPassword = config["AdminSettings:AdminPassword"];

            if (adminEmail != null && adminPassword != null)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var userManager = scope.ServiceProvider
                        .GetRequiredService<UserManager<RestaurantUser>>();
                    var roleManager = scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

                    if (!roleManager.Roles.Any())
                    {
                        IdentityRole roleAdmin = new(UserRoles.Admin);
                        IdentityRole roleUser = new(UserRoles.User);

                        await roleManager.CreateAsync(roleAdmin);
                        await roleManager.CreateAsync(roleUser);
                    }

                    RestaurantUser? user = await userManager.FindByEmailAsync(adminEmail);
                    if (user == null)
                    {
                        user = new RestaurantUser()
                        {
                            Email = adminEmail,
                            UserName = adminEmail
                        };
                        await userManager.CreateAsync(user, adminPassword);
                        await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }

                }
            }
            else
            {
                throw new Exception("Admin name or password were not provided");
            }
        }
    }
}
