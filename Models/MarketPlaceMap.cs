using System.ComponentModel.DataAnnotations;
using TrackAll_Backend.Database;

namespace TrackAll_BackEnd.Models
{
    public class MarketPlaceMap
    {
        [Key]
        public Guid Id { get; set; }
        public IdentityModel Restaurant { get; set; }
        public Guid Zomato { get; set; }
        public Guid Swiggy { get; set; }
        public Guid UberEats { get; set; }
        public Guid FoodPanda { get; set; }
    }
}
