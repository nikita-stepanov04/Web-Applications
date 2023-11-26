using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.IdentityContext;
using RestaurantApi.Models.OrderModels;
using RestaurantApi.Repository.Interfaces;
using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;
        private UserManager<RestaurantUser> _userManager;

        public OrderController(
            IOrderRepository orderRepository,
            UserManager<RestaurantUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder(Order order)
        {
            var user = await _userManager.GetUserAsync(this.User);
            order.UserId = user?.Id;

            return await _orderRepository.AddOrderAsync(order)
                ? Ok()
                : BadRequest("Something went wrong");
        }

        [HttpGet("all")]
        public async Task<List<Order>> Orders() =>
            await _orderRepository.GetAllNotCompletedOrdersAsync();

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> OrdersForUser()
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user != null)
            {
                return Ok(await _orderRepository.OrdersForUserAsync(user.Id));
            }
            return Unauthorized();
        }
    }
}