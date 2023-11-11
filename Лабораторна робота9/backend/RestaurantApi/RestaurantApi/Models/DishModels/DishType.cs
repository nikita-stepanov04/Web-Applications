using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.DishModels
{
    public class DishType
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Dish type name is not defined")]
        [StringLength(30)]
        public string? Name { get; set; }
    }
}