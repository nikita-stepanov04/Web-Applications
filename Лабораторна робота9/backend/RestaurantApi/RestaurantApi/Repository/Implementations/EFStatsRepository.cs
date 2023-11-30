using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Models.StatsModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EFStatsRepository : IStatsRepository
    {
        private DataContext _context;

        public EFStatsRepository(DataContext context) => 
            _context = context;

        public async Task<decimal> GetAverageCartPrice()
        {
            return await _context.Orders.SumAsync(o => o.TotalPrice) /
               await _context.Orders.CountAsync();
        }

        public async Task<decimal> GetAverageDishInCartPrice()
        {
            return await _context.CartLines
                .Include(l => l.Dish)
                .Select(l => l.Dish!)
                .SumAsync(d => d.Price) / await _context.CartLines.CountAsync();
        }

        public async Task<List<EntityPopularity>> GetDishPopularity()
        {
            List<EntityPopularity> dishesPopularity = new();
            List<Dish> dishes = await _context.Dishes.ToListAsync();

            foreach(Dish dish in dishes)
            {
                dishesPopularity.Add(new()
                {
                    EntityName = dish.Name,
                    Popularity = await _context.CartLines
                                    .Include(l => l.Dish)
                                    .Where(l => l.Dish!.Id == dish.Id)
                                    .SumAsync(l => l.Quantity)
                });
            }
            return dishesPopularity;
        }

        public async Task<List<EntityPopularity>> GetDishTypesPopularity()
        {
            List<EntityPopularity> dishTypesPopularity = new();
            List<DishType> dishTypes = await _context.DishTypes.ToListAsync();

            foreach (DishType type in dishTypes)
            {
                dishTypesPopularity.Add(new()
                {
                    EntityName = type.Name,
                    Popularity = await _context.CartLines
                                    .Include(l => l.Dish!)
                                        .ThenInclude(d => d.DishType)
                                    .Where(l => l.Dish!.DishTypeId == type.Id)
                                    .SumAsync(l => l.Quantity)
                });
            }
            return dishTypesPopularity;
        }
    }
}
