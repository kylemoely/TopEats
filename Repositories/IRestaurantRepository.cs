using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetRestaurantById(int restaurantId);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task CreateRestaurant(Restaurant restaurant);
        Task UpdateRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(int restaurantId);
    }
}