using System;

namespace TopEats.Models
{
    public class Restaurant
    {
        public Guid? RestaurantId { get; set; } //  PRIMARY KEY
        public string RestaurantName { get; set; }
        public string Cuisine { get; set; }
        public int PriceCategory { get; set; }

        public Restaurant(Guid restaurantId, string restaurantName, string cuisine, int priceCategory)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            Cuisine = cuisine;
            PriceCategory = priceCategory;
        }

        public Restaurant(string restaurantName, string cuisine, int priceCategory)
        {
            RestaurantName = restaurantName;
            Cuisine = cuisine;
            PriceCategory = priceCategory;
        }
    }
}