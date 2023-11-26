using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.SheduleModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private IScheduleRepository _scheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository) => 
            _scheduleRepository = scheduleRepository;

        [HttpGet]
        public async Task<IActionResult> GetShedule()
        {
            Schedule? schedule = await _scheduleRepository.GetSheduleAsync();
            return schedule != null
                ? Ok(schedule)
                : NotFound();
        }
    }
}
