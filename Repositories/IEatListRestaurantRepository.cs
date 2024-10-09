using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IEatListRestaurantRepository
    {
        Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(int eatListId);
    }
}