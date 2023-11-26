using RestaurantApi.Models.IdentityContext;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.BindingTargets
{
    public class UserUpdateModel
    {
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [RegularExpression(@"^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$")]
        public string? PhoneNumber { get; set; }

        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? Name { get; set; }

        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? Surname { get; set; }

        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? City { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Street { get; set; }

        [Range(1, int.MaxValue)]
        public int? HouseNumber { get; set; }
    }
}
