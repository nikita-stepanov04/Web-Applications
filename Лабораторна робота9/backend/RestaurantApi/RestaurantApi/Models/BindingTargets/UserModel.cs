using RestaurantApi.Models.IdentityContext;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.BindingTargets
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,100}$",
            ErrorMessage = "Password must contain one lowercase, one uppercase character, nubmer and one non-alhpanumeric character")]
        public string? Password { get; set; }

        [RegularExpression(@"^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? Name { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 1)]
        [RegularExpression(@"^(?!.*\d.*\d)(?! +$)[\p{L}\s\-'\.]+")]
        public string? Surname { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? City { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public string? Street { get; set; }

        [Range(1, int.MaxValue)]
        public int? Flat { get; set; }

        public RestaurantUser ToRestaurantUser() =>
            new RestaurantUser()
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                UserName = this.Email,
                PhoneNumber = this.PhoneNumber,
                Birthday = this.Birthday,
                Gender = this.Gender,
                City = this.City,
                Street = this.Street,
                Flat = this.Flat
            };
    }
}
