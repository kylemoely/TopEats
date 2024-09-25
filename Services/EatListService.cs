using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class EatListService : IEatListService
    {
        private readonly IEatListRepository _eatListRepository;

        public EatListService(IEatListRepository eatListRepository)
        {
            _eatListRepository = eatListRepository;
        }

        public async Task<EatList> GetEatListById(int eatListId)
        {
            return await _eatListRepository.GetEatListById(eatListId);
        }

        public async Task<IEnumerable<EatList>> GetAllEatLists()
        {
            return await _eatListRepository.GetAllEatLists();
        }

        public Task CreateEatList(EatList eatList)
        {
            await _eatListRepository.CreateEatList(eatList);
        }

        public Task UpdateEatList(EatList eatList)
        {
            await _eatListRepository.UpdateEatList(eatList);
        }

        public Task DeleteEatList(int eatListId)
        {
            await _eatListRepository.DeleteEatList(eatListId);
        }
    }
}