using Microsoft.AspNetCore.JsonPatch;
using RestaurantApi.Models.SheduleModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IScheduleRepository
    {
        Task<Schedule?> GetSheduleAsync();

        Task<bool> PatchSchedule(JsonPatchDocument<Schedule> patchDoc);
    }
}
