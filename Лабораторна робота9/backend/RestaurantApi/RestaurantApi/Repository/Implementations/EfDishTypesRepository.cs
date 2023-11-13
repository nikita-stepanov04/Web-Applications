using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EfDishTypesRepository : IDishTypesRepository
    {
        private DataContext _context;

        public EfDishTypesRepository(DataContext context) =>
            _context = context;

        public IQueryable<DishType> DishTypes =>
            _context.DishTypes;
    }
}
