using Microsoft.AspNetCore.Identity;

namespace RestaurantApi.Models.DataContext
{
    public static class SeedIdentity
    { 
        public static async void SeedDatabase(IApplicationBuilder app, IConfiguration config)
        { 
            string? adminUser = config["AdminSettings:AdminUser"];
            string? adminPassword = config["AdminSettings:AdminPassword"];

            await Console.Out.WriteLineAsync($"name: {adminUser}");
            await Console.Out.WriteLineAsync($"password: {adminPassword}");

            if (adminUser != null && adminPassword != null)
            {
                UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityUser? user = await userManager.FindByNameAsync(adminUser);
                if (user == null)
                {
                    user = new IdentityUser(adminUser);
                    user.Email = "admin@example.com";
                    user.PhoneNumber = "555-1234";
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
