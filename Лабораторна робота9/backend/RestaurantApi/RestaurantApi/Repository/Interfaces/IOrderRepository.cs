using RestaurantApi.Models.OrderModels;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        Task<bool> AddOrderAsync(Order order);

        Task<List<Order>> OrdersForUserAsync(string userId);

        Task<List<Order>> GetAllNotCompletedOrdersAsync();
    }
}
