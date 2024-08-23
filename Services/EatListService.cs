using System;
using System.Collections.Generic;
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

        public EatList GetEatListById(int eatListId)
        {
            return _eatListRepository.GetEatListById(eatListId);
        }

        public IEnumerable<EatList> GetAllEatLists()
        {
            return _eatListRepository.GetAllEatLists();
        }

        public void CreateEatList(EatList eatList)
        {
            return _eatListRepository.CreateEatList(eatList);
        }

        public void UpdateEatList(EatList eatList)
        {
            return _eatListRepository.UpdateEatList(eatList);
        }

        public void DeleteEatList(int eatListId)
        {
            return _eatListRepository.DeleteEatList(eatListId);
        }
    }
}