using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IEatListService
    {
        EatList GetEatListById(int eatListId);
        IEnumerable<EatList> GetAllEatLists();
        void CreateEatList(EatList eatList);
        void UpdateEatList(EatList eatList);
        void DeleteEatList(int eatListId);
    }
}