using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Models.IdentityContext
{
    public class IdentityContext : IdentityDbContext<RestaurantUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> opts)
            : base(opts) { }
    }
}
