using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class UserFavRestaurantService : IUserFavRestaurantService
    {
        private readonly IUserFavRestaurantRepository _userFavRestaurantRepository;

        public UserFavRestaurantService(IUserFavRestaurantRepository userFavRestaurantRepository)
        {
            _userFavRestaurantRepository = userFavRestaurantRepository;
        }

        public async Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(int userId)
        {
            return await _userFavRestaurantRepository.GetUserTopRestaurants(userId);
        }
    }
}