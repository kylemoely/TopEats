using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IEatListRepository
    {
        Task<EatList> GetEatListById(int eatListId);
        Task<IEnumerable<EatList>> GetAllEatLists();
        Task CreateEatList(EatList eatList);
        Task UpdateEatList(EatList eatList);
        Task DeleteEatList(int eatListId);
        Task<IEnumerable<EatList>> GetUserEatLists(int userId);
    }
}
