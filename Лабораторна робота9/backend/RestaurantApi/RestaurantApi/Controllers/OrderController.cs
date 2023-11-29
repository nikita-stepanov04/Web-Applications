using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.IdentityContext;
using RestaurantApi.Models.OrderModels;
using RestaurantApi.Repository.Interfaces;
using System.Data;
using System.Text.Json;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> OrderById(long id)
        {
            Order? order = await _orderRepository.GetOrderById(id);
            return order != null
                ? Ok(order)
                : NotFound($"Order with id: {id} was not found");
        }

        [HttpGet("all-non-completed")]
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("complete/{id}")]
        public async Task<IActionResult> CompleteOrder(long id)
        {
            bool result = await _orderRepository.CompleteOrderAsync(id);
            return result
                ? Ok()
                : NotFound($"Order with id: {id} was not found");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveOrder(long id)
        {
            bool result = await _orderRepository.RemoveOrderAsync(id);
            return result
                ? Ok()
                : NotFound($"Order with id: {id} was not found");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("remove/line/{id}")]
        public async Task<IActionResult> RemoveLine(long id)
        {
            bool result = await _orderRepository.RemoveCartLineFromOrderAsync(id);
            return result
                ? Ok()
                : NotFound($"Line with id: {id} was not found");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add/line")]
        public async Task<IActionResult> AddLine(
            NewLine newLine)
        {
            bool result = await _orderRepository
                .AddCartLineToOrderAsync(newLine.OrderId,
                    newLine.DishId, newLine.Quantity);
            return result
                ? Ok()
                : BadRequest();
        }
        
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("patch/{orderId:long}")]
        public async Task<IActionResult> PatchOrder(
            JsonPatchDocument<Order> patchDoc, long orderId)
        {
            bool result = await _orderRepository
                .PatchOrderAsync(patchDoc, orderId);
            return result
                ? Ok()
                : BadRequest();
        }
    }

    public class NewLine
    {
        public long OrderId { get; set; }
        public long DishId { get; set; }
        public int Quantity { get; set; }
    }
}