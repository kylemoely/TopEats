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

        public async Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(Guid userId)
        {
            return await _userFavRestaurantRepository.GetUserTopRestaurants(userId);
        }

        public async Task CreateUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            await _userFavRestaurantRepository.CreateUserTopRestaurant(userFavRestaurant);
        }

        public async Task UpdateUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            await _userFavRestaurantRepository.UpdateUserTopRestaurant(userFavRestaurant);
        }

        public async Task DeleteUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            await _userFavRestaurantRepository.DeleteUserTopRestaurant(userFavRestaurant);
        }
    }
}
