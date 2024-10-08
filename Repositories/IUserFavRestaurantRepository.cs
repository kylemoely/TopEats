using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IUserFavRestaurantRepository
    {
        Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(int userId);
    }
}
