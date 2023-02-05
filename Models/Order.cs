using System.ComponentModel.DataAnnotations;
using TrackAll_Backend.Database;

namespace MarketPlace_Orders.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } //Id given by TrackALL
        public Guid OrderId { get; set; } //Id given by MarketPlace
        public string CustomerName { get; set; }
        public string Location { get; set; }
        public string DeliveryBoyName { get; set; }
        public string BoyPhone { get; set; }
        public string MarketPlaceName { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }
        public DateTime OrderTime { get; set; }
        public int? OrderNo { get; set; }
        public int ToPrepare { get; set; }
        public IdentityModel Restaurant { get; set; }
    }
}
