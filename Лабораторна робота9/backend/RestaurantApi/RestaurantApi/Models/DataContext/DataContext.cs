using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Models.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }

        public DbSet<Dish> Dishes => Set<Dish>();
        public DbSet<DishType> DishTypes => Set<DishType>();
        public DbSet<Image> Images => Set<Image>();
    }
}
