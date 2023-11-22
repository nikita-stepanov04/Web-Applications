using Microsoft.AspNetCore.Identity;
using RestaurantApi.Models.IdentityContext;
using System.Text.Json;

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
                        await Console.Out.WriteLineAsync(JsonSerializer.Serialize(user));
                        var result = await userManager.CreateAsync(user, adminPassword);
                        var result1 = await userManager.AddToRoleAsync(user, UserRoles.Admin);
                        await Console.Out.WriteLineAsync($"\n\n----{result}, {result1}--------\n\n");
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
