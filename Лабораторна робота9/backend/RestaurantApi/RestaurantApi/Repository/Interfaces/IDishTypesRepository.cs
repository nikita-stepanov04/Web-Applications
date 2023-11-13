using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IDishTypesRepository
    {
        IQueryable<DishType> DishTypes { get; }
    }
}
