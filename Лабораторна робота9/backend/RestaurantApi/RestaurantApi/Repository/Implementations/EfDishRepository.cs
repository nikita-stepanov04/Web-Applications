using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EfDishRepository : IDishRepository
    {
        private DataContext _context;

        public EfDishRepository(DataContext context) =>
            _context = context;

        public IQueryable<Dish> Dishes =>
            _context.Dishes;

        public async Task<bool> AddDishAsync(Dish d)
        {
            await _context.AddAsync(d);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDishAsync(Dish d)
        {
            _context.Remove(d);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Dish?> GetDishById(long id) =>
            _context.Dishes.FirstOrDefaultAsync(d => d.Id == id);

        public Task<string?> GetDishDescriptionById(long id) =>
            _context.Dishes
                .Where(d => d.Id == id)
                .Select(d => d.Description)
                .FirstOrDefaultAsync();

        public async Task<List<Dish>> GetDishesByTypeAndPagingInfoAsync(
            long? dishTypeId, PagingInfo pagInfo)
        {
            return await _context.Dishes
                .Where(d => dishTypeId == null || d.DishTypeId == dishTypeId)
                .OrderBy(d => d.Name)
                .Skip((pagInfo.CurrentPage - 1) * pagInfo.ItemsPerPage)
                .Take(pagInfo.ItemsPerPage)
                .Select(d => new Dish() 
                    {
                        Id = d.Id,
                        Name = d.Name,
                        DishTypeId = d.DishTypeId,
                        Price = d.Price,
                        ImageId = d.ImageId
                    })
                .ToListAsync();
        }

        public async Task<int> GetTotalPagesAsync(
            long? dishTypeId, PagingInfo pagInfo)
        {
            return (int)Math.Ceiling((decimal)
                await _context.Dishes.CountAsync(d =>
                    dishTypeId == null || d.DishTypeId == dishTypeId) /
                        pagInfo.ItemsPerPage);
        }
    }
}
