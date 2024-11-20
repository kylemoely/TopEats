using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Restaurant> GetRestaurantById(Guid restaurantId)
        {
            return await _restaurantRepository.GetRestaurantById(restaurantId);
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _restaurantRepository.GetAllRestaurants();
        }

        public async Task<Restaurant> CreateRestaurant(Restaurant restaurant)
        {
            return await _restaurantRepository.CreateRestaurant(restaurant);
        }

        public async Task UpdateRestaurant(Restaurant restaurant)
        {
            await _restaurantRepository.UpdateRestaurant(restaurant);
        }

        public async Task DeleteRestaurant(Guid restaurantId)
        {
            await _restaurantRepository.DeleteRestaurant(restaurantId);
        }
    }
}