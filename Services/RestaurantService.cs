using System;
using System.Collections.Generic;
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

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return _restaurantRepository.GetRestaurantById(restaurantId);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAllRestaurants();
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.CreateRestaurant(restaurant);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.UpdateRestaurant(restaurant);
        }

        public void DeleteRestaurant(int restaurantId)
        {
            return _restaurantRepository.DeleteRestaurant(restaurantId);
        }
    }
}