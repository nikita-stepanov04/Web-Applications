using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApi.Models.DataContext
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    { 
        public IdentityContext(DbContextOptions<IdentityContext> opts)
            :base(opts) { }
    }
}
