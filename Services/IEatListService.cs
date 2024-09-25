using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IEatListService
    {
        Task<EatList> GetEatListById(int eatListId);
        Task<IEnumerable<EatList>> GetAllEatLists();
        Task CreateEatList(EatList eatList);
        Task UpdateEatList(EatList eatList);
        Task DeleteEatList(int eatListId);
    }
}