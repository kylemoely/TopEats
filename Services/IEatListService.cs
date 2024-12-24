using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IEatListService
    {
        Task<EatList> GetEatListById(Guid eatListId);
        Task<IEnumerable<EatList>> GetAllEatLists();
        Task<EatList> CreateEatList(EatList eatList);
        Task UpdateEatList(EatList eatList);
        Task DeleteEatList(Guid eatListId);
        Task<IEnumerable<EatList>> GetUserEatLists(Guid userId);
    }
}
