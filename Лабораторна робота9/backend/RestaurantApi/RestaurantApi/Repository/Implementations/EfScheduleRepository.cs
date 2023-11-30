using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.SheduleModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EfScheduleRepository : IScheduleRepository
    {
        private DataContext _context;

        public EfScheduleRepository(DataContext context) => 
            _context = context;

        public async Task<Schedule?> GetSheduleAsync() =>
            await _context.Schedule.FirstOrDefaultAsync();

        public async Task<bool> PatchSchedule(JsonPatchDocument<Schedule> patchDoc)
        {
            Schedule? schedule = await _context.Schedule.FirstOrDefaultAsync();
            if (schedule != null)
            {
                patchDoc.ApplyTo(schedule);
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
