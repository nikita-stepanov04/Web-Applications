using Microsoft.AspNetCore.JsonPatch;
using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IDishTypesRepository
    {
        IQueryable<DishType> DishTypes { get; }

        Task<bool> AddDishType(string name);

        Task<bool> PatchDishTypes(JsonPatchDocument<List<DishType>> patchDoc);

        Task<bool> DeleteDishType(long id);
    }
}
