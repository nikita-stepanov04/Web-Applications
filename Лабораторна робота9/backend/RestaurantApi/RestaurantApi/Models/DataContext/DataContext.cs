using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Models.OrderModels;
using RestaurantApi.Models.SheduleModels;

namespace RestaurantApi.Models.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }

        public DbSet<Dish> Dishes => Set<Dish>();
        public DbSet<DishType> DishTypes => Set<DishType>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<CartLine> CartLines => Set<CartLine>();
        public DbSet<Schedule> Schedule => Set<Schedule>();
    }
}
