using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TrackAll_Backend.Database
{
    public class IdentityModel : IdentityUser
    {
        [Key]
        public Guid RestId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RestaurantName { get; set; }
    }
}
