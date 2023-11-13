using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IDishRepository
    {
        IQueryable<Dish> Dishes { get; }

        Task<bool> AddDishAsync(Dish d);
        Task<bool> DeleteDishAsync(Dish d);
    }
}
