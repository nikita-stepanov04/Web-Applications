using RestaurantApi.Models.DishModels;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.BindingTargets
{
    public class DishFormData
    {
        [Required]
        public IFormFile? Image { get; set; }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }

        [Required]
        [StringLength(512)]
        public string? Description { get; set; }

        [Required]
        public long DishTypeId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public Dish ToDish() =>
            new Dish()
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price
            };
    }
}
