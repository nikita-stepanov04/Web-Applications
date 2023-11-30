using RestaurantApi.Models.StatsModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IStatsRepository
    {
        Task<List<EntityPopularity>> GetDishPopularity();

        Task<List<EntityPopularity>> GetDishTypesPopularity();

        Task<decimal> GetAverageDishInCartPrice();

        Task<decimal> GetAverageCartPrice();
    }
}
