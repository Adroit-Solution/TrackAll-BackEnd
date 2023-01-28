namespace TrackAll_BackEnd.HelperModels
{
    public class MarketPlaceConnection
    {
        public string RestaurantEmail { get; set; }
        public Guid Zomato { get; set; }
        public Guid Swiggy { get; set; }
        public Guid UberEats { get; set; }
        public Guid FoodPanda { get; set; }
    }
}
