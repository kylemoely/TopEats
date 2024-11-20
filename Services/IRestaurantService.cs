using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantById(Guid restaurantId);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> CreateRestaurant(Restaurant restaurant);
        Task UpdateRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(Guid restaurantId);
    }
}