using RestaurantApi.Models.SheduleModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IScheduleRepository
    {
        Task<Schedule?> GetSheduleAsync();
    }
}
