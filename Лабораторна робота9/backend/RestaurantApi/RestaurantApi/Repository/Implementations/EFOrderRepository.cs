using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Models.OrderModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EFOrderRepository : IOrderRepository
    {
        private DataContext _context;

        public EFOrderRepository(DataContext context) =>
            _context = context;

        public IQueryable<Order> Orders =>
            _context.Orders;

        public async Task<bool> AddOrderAsync(Order order)
        {
            if (order.CartLines?.Any() ?? false)
            {
                IEnumerable<long> dishIds = order.CartLines!
                .Select(line => line.DishId);

                IEnumerable<Dish> dishes = _context.Dishes
                    .Where(d => dishIds.Contains(d.Id));

                if (dishes.Count() == dishIds.Count())
                {
                    order.TotalPrice = dishes.Sum(dish =>
                        dish.Price * order.CartLines.First(line =>
                            line.DishId == dish.Id).Quantity);

                    order.PurchaseDate = DateTime.Now;

                    await _context.Orders.AddAsync(order);
                    return await _context.SaveChangesAsync() > 0;
                }
            }
            return false;
        }

        public async Task<List<Order>> OrdersForUserAsync(string userId)
        {
            return await GetAllOrders()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllNotCompletedOrdersAsync()
        {
            return await GetAllOrders()
                .Where(o => !o.IsCompleted)
                .ToListAsync();
        }

        private IQueryable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.CartLines!)
                .ThenInclude(c => c.Dish!)
                .Select(o => new Order
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CustomerName = o.CustomerName,
                    CustomerSurname = o.CustomerSurname,
                    PurchaseDate = o.PurchaseDate,
                    TotalPrice = o.TotalPrice,
                    City = o.City,
                    Street = o.Street,
                    HouseNumber = o.HouseNumber,
                    PhoneNumber = o.PhoneNumber,
                    IsCompleted = o.IsCompleted,
                    CartLines = o.CartLines!.Select(line => new CartLine
                    {
                        Quantity = line.Quantity,
                        Dish = new Dish
                        {
                            Name = line.Dish.Name,
                            Price = line.Dish.Price
                        }
                    })

                })
                .OrderBy(o => o.IsCompleted)
                .ThenBy(o => o.PurchaseDate);
        }

    }
}
