using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IFollowRepository
    {
        Task<IEnumerable<Follow>> GetUserFollowers(Guid userId);
        Task<IEnumerable<Follow>> GetUserFollowees(Guid userId);
        Task CreateFollow(Follow follow);
        Task DeleteFollow(Follow follow);
    }
}
