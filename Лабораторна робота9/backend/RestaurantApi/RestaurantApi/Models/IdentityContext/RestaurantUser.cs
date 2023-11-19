using Microsoft.AspNetCore.Identity;

namespace RestaurantApi.Models.IdentityContext
{
    public class RestaurantUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? Flat { get; set; }
    }
}
