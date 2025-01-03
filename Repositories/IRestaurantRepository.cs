using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetRestaurantById(Guid restaurantId);
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> CreateRestaurant(Restaurant restaurant);
        Task UpdateRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(Guid restaurantId);
    }
}