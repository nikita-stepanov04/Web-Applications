using Microsoft.AspNetCore.Identity;
using RestaurantApi.Models.BindingTargets;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models.IdentityContext
{
    public class RestaurantUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? Flat { get; set; }

        public UserModel ToUserModel() =>
        new()
        {
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            Name = this.Name,
            Surname = this.Surname,
            Birthday = this.Birthday,
            Gender = this.Gender,
            City = this.City,
            Street = this.Street,
            Flat = this.Flat
        };
    }    
}
