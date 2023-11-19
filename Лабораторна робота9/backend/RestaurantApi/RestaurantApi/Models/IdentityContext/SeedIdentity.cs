using Microsoft.AspNetCore.Identity;
using RestaurantApi.Models.IdentityContext;

namespace RestaurantApi.Models.DataContext
{
    public static class SeedIdentity
    { 
        public static async void SeedDatabase(IApplicationBuilder app, IConfiguration config)
        { 
            string? adminUser = config["AdminSettings:AdminUser"];
            string? adminPassword = config["AdminSettings:AdminPassword"];

            if (adminUser != null && adminPassword != null)
            {
                UserManager<RestaurantUser> userManager = app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<RestaurantUser>>();

                RestaurantUser? user = await userManager.FindByNameAsync(adminUser);
                if (user == null)
                {
                    user = new RestaurantUser() 
                    {
                        UserName = adminUser,
                        PhoneNumber = "555-1234"
                    };                    
                    await userManager.CreateAsync(user, adminPassword);
                }
            }
            else
            {
                throw new Exception("Admin name or password were not provided");
            }
        }
    }
}
