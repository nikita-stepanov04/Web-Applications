using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.IdentityContext;
using RestaurantApi.Models.StatsModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatsController : ControllerBase
    {
        private IStatsRepository _statsRepository;

        public StatsController(IStatsRepository statsRepository) =>
            _statsRepository = statsRepository;

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("dish")]
        public async Task<List<EntityPopularity>> GetDishPopularity() =>
            await _statsRepository.GetDishPopularity();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("dishtypes")]
        public async Task<List<EntityPopularity>> GetDishTypesPopularity() =>
            await _statsRepository.GetDishTypesPopularity();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("dish-in-cart-price")]
        public async Task<decimal> GetAverageDishInCartPrice() =>
            await _statsRepository.GetAverageDishInCartPrice();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("cart")]
        public async Task<decimal> GetAverageCartPrice() =>
            await _statsRepository.GetAverageCartPrice();
    }
}
