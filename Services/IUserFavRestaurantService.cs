using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IUserFavRestaurantService
    {
        Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(int userId);
    }
}