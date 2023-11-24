using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IDishRepository
    {
        IQueryable<Dish> Dishes { get; }

        Task<bool> AddDishAsync(Dish d);
        Task<bool> DeleteDishAsync(Dish d);

        Task<Dish?> GetDishById(long id);

        Task<string?> GetDishDescriptionById(long id);

        Task<List<Dish>> GetDishesByTypeAndPagingInfoAsync(
            long? dishTypeId, PagingInfo pagInfo);

        Task<int> GetTotalPagesAsync(long? dishTypeId, PagingInfo pagInfo);
    }
}
