using System.ComponentModel.DataAnnotations;
using TrackAll_Backend.Database;

namespace TrackAll_BackEnd.Models
{
    public class ProductModel
    {
        [Key]
        public Guid Id { get; set; }
        public IdentityModel Restaurant { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Stock { get; set; }
        public string Category { get; set; }
    }
}
