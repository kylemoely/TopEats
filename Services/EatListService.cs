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

        public async Task<EatList> GetEatListById(Guid eatListId)
        {
            return await _eatListRepository.GetEatListById(eatListId);
        }

        public async Task<IEnumerable<EatList>> GetAllEatLists()
        {
            return await _eatListRepository.GetAllEatLists();
        }

        public async Task CreateEatList(EatList eatList)
        {
            await _eatListRepository.CreateEatList(eatList);
        }

        public async Task UpdateEatList(EatList eatList)
        {
            await _eatListRepository.UpdateEatList(eatList);
        }

        public async Task DeleteEatList(Guid eatListId)
        {
            await _eatListRepository.DeleteEatList(eatListId);
        }

        public async Task<IEnumerable<EatList>> GetUserEatLists(Guid userId)
        {
            return await _eatListRepository.GetUserEatLists(userId);
        }
    }
}
