using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models.OrderModels
{
    public class Order
    { 
        public long Id { get; set; }
        public string? UserId { get; set; }

        [Required(ErrorMessage = "No one dish was added to order")]
        public IEnumerable<CartLine>? CartLines { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }

        public bool IsCompleted { get; set; }
    }
}
