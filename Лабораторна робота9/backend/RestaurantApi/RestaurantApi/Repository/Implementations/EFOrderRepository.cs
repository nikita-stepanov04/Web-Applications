using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Models.OrderModels;
using RestaurantApi.Repository.Interfaces;
using System.Text.Json;

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
                .OrderBy(o => o.IsCompleted)
                .ThenByDescending(o => o.PurchaseDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllNotCompletedOrdersAsync()
        {
            return await GetAllOrders()
                .Where(o => !o.IsCompleted)
                .OrderBy(o => o.IsCompleted)
                .ThenByDescending(o => o.PurchaseDate)
                .ToListAsync();
        }        

        public async Task<bool> CompleteOrderAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.IsCompleted = true;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveOrderAsync(long id)
        {
            var order = await _context.Orders
                .Include(o => o.CartLines)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                _context.RemoveRange(order.CartLines!);
                _context.Remove(order);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Order?> GetOrderById(long id)
        {
            return await GetAllOrders()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> RemoveCartLineFromOrderAsync(long lineId)
        {
            CartLine? cartLine = await _context.CartLines
                .Where(l => l.Id == lineId)
                .Include(l => l.Dish)
                .FirstOrDefaultAsync();
            await Console.Out.WriteLineAsync($"\n\n {JsonSerializer.Serialize(cartLine)} \n\n");
            if (cartLine != null)
            {
                _context.CartLines.Remove(cartLine);
                Order? order = await _context.Orders.FindAsync(cartLine.OrderId);
                await Console.Out.WriteLineAsync($"\n\n {JsonSerializer.Serialize(order)} \n\n");
                if (order != null)
                {
                    order.TotalPrice -= cartLine.Quantity * cartLine.Dish!.Price;
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddCartLineToOrderAsync(
            long orderId, long dishId, int quantity)
        {
            Order? order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.CartLines!)
                    .ThenInclude(l => l.Dish)
                .FirstOrDefaultAsync();

            if (order != null && quantity >= 1)
            {
                Dish? dish = await _context.Dishes.FindAsync(dishId);
                if (dish != null)
                {
                    List<CartLine> cartLines = order.CartLines!.ToList();
                    CartLine? existingLine = cartLines
                        .FirstOrDefault(line => line.DishId == dish.Id);

                    if (existingLine != null)
                    {
                        existingLine.Quantity += quantity;
                    }
                    else
                    {
                        cartLines.Add(new CartLine()
                        {
                            Dish = dish,
                            Quantity = quantity
                        });
                    }

                    order.TotalPrice = cartLines
                        .Sum(line => line.Quantity * line.Dish!.Price);
                    order.CartLines = cartLines;                    
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PatchOrderAsync(
            JsonPatchDocument<Order> patchDoc, long orderId)
        {
            Order? order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.CartLines!)
                    .ThenInclude(l => l.Dish)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                bool patchResult = true;
                order.CartLines = order.CartLines!.ToList();
                patchDoc.ApplyTo(order, (error) => patchResult = false);
                if (!patchResult)
                {
                    return false;
                }
                order.TotalPrice = order.CartLines!
                    .Sum(line => line.Quantity * line.Dish!.Price);
            }
            return await _context.SaveChangesAsync() > 0;
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
                        Id = line.Id,
                        Quantity = line.Quantity,
                        Dish = new Dish
                        {
                            Id = line.Dish.Id,
                            Name = line.Dish.Name,
                            Price = line.Dish.Price
                        }
                    })

                });
        }        
    }
}
