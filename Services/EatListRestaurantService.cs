using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class EatListRestaurantService : IEatListRestaurantService
    {
        private readonly IEatListRestaurantRepository _eatListRestaurantRepository;

        public EatListRestaurantService(IEatListRestaurantRepository eatListRestaurantRepository)
        {
            _eatListRestaurantRepository = eatListRestaurantRepository;
        }

        public async Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(int eatListId)
        {
            return await _eatListRestaurantRepository.GetEatListRestaurants(eatListId);
        }

        public async Task AddRestaurantToEatList(int eatListId, int restaurantId)
        {
            await _eatListRestaurantRepository.AddRestaurantToEatList(eatListId, restaurantId);
        }
    }
}
