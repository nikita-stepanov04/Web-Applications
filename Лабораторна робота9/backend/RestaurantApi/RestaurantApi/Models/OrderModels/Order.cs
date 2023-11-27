using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models.OrderModels
{
    public class Order
    { 
        public long Id { get; set; }
        public string? UserId { get; set; }

        public IEnumerable<CartLine>? CartLines { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Required]
        [RegularExpression(@"^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? CustomerName { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? CustomerSurname { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string? City { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string? Street { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int? HouseNumber { get; set; }

        public bool IsCompleted { get; set; }
    }
}
