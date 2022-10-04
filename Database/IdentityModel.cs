using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TrackAll_Backend.Database
{
    public class IdentityModel:IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
