using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IRestaurantService
    {
        Restaurant GetRestaurantById(int restaurantId);
        IEnumerable<Restaurant> GetAllRestaurants();
        void CreateRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(int restaurantId);
    }
}