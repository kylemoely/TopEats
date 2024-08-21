using System;

namespace TopEats.Models
{
    public class Restaurant
    {
        public int? restaurantId { get; set; } //  PRIMARY KEY
        public string restaurantName { get; set; }
        public string cuisine { get; set; }
        public int priceCategory { get; set; }

        public Restaurant(int _restaurantId, string _restaurantName, string _cuisine, int _priceCategory)
        {
            restaurantId = _restaurantId
            restaurantName = _restaurantName
            cuisine = _cuisine
            priceCategory = _priceCategory
        }

        public Restaurant(string _restaurantName, string _cuisine, int _priceCategory)
        {
            restaurantName = _restaurantName
            cuisine = _cuisine
            priceCategory = _priceCategory
        }
    }
}