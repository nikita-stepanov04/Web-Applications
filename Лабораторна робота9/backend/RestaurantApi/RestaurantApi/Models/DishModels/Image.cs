using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class Image
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Image bit map is not defined")]
        public byte[]? BitMap { get; set; }

        [Required(ErrorMessage = "Image content type is not defined")]
        [StringLength(64)]
        public string? ContentType { get; set; }
    }
}
