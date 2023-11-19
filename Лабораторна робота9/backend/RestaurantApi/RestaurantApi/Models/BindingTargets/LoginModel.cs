using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.BindingTargets
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is not defined")]
        [StringLength(256, MinimumLength = 3)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is not defined")]
        [StringLength(256, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,100}$",
            ErrorMessage = "Password must contain one lowercase, one uppercase character, nubmer and one non-alhpanumeric character")]
        public string? Password { get; set; }
    }
}