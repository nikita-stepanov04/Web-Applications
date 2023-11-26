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

        Task<List<Dish>> GetDishesByTypeAsync(
            long? dishTypeId, PagingInfo pagInfo);

        Task<List<Dish>> GetDishesBySubstringAsync(
            string substring, PagingInfo pagInfo);

        Task<int> GetTotalPagesAsync(long? dishTypeId, string? substring, PagingInfo pagInfo);
    }
}
