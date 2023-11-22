using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models.BindingTargets
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is not defined")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is not defined")]        
        public string? Password { get; set; }
    }
}