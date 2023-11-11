using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models.DishModels
{
    public class Dish
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is not defined")]
        [StringLength(30)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is not defined")]
        [StringLength(512)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Dish type is not defined")]
        public DishType? DishType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is not defined")]
        public Image? Image { get; set; }
        public long ImageId { get; set; }
    }
}
