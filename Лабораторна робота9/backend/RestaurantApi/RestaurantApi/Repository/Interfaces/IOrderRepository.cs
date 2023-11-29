using Microsoft.AspNetCore.JsonPatch;
using RestaurantApi.Models.OrderModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        Task<bool> AddOrderAsync(Order order);

        Task<List<Order>> OrdersForUserAsync(string userId);

        Task<List<Order>> GetAllNotCompletedOrdersAsync();

        Task<Order?> GetOrderById(long id);

        Task<bool> CompleteOrderAsync(long id);

        Task<bool> RemoveOrderAsync(long id);

        Task<bool> RemoveCartLineFromOrderAsync(long lineId);

        Task<bool> AddCartLineToOrderAsync(long orderId, long dishId, int quantity);

        Task<bool> PatchOrderAsync(JsonPatchDocument<Order> patchDoc, long orderId);
    }
}
