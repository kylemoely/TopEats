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

        public async Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(Guid eatListId)
        {
            return await _eatListRestaurantRepository.GetEatListRestaurants(eatListId);
        }

        public async Task AddRestaurantToEatList(EatListRestaurant eatListRestaurant)
        {
            await _eatListRestaurantRepository.AddRestaurantToEatList(eatListRestaurant);
        }

        public async Task DeleteRestaurantFromEatList(EatListRestaurant eatListRestaurant)
        {
            await _eatListRestaurantRepository.DeleteRestaurantFromEatList(eatListRestaurant);
        }
    }
}
