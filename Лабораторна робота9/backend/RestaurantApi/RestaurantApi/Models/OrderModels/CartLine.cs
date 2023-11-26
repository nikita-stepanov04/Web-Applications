using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Models.OrderModels
{
    public class CartLine
    {
        public long Id { get; set; }
        public long DishId { get; set; }
        public Dish? Dish { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
