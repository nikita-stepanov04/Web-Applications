using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.SheduleModels
{
    public class Schedule
    {
        public long Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Monday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Tuesday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Wednesday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Thursday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Friday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Saturday { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2} - \d{1,2}:\d{2}$")]
        public string? Sunday { get; set; }
    }
}
