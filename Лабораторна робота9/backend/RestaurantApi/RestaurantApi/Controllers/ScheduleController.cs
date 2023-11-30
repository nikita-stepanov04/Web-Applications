using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.IdentityContext;
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch]
        public async Task<IActionResult> PatchSchedule(
            JsonPatchDocument<Schedule> patchDoc)
        {
            return await _scheduleRepository.PatchSchedule(patchDoc)
                ? Ok()
                : BadRequest();
        }
    }
}
