using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IEatListRestaurantService
    {
        Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(Guid eatListId);
        Task AddRestaurantToEatList(EatListRestaurant eatListRestaurant);
        Task DeleteRestaurantFromEatList(EatListRestaurant eatListRestaurant);
    }
}
