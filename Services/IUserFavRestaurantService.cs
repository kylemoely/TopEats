using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IUserFavRestaurantService
    {
        Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(Guid userId);
        Task CreateUserTopRestaurant(UserFavRestaurant userFavRestaurant);
        Task UpdateUserTopRestaurant(UserFavRestaurant userFavRestaurant);
        Task DeleteUserTopRestaurant(UserFavRestaurant userFavRestaurant);
    }
}
