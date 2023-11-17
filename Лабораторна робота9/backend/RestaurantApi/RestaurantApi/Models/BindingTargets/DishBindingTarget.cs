using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Models.BindingTargets
{
    public class DishBindingTarget
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DishType? DishType { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        public Dish ToDish() =>
            new Dish()
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price
            };
    }
}
