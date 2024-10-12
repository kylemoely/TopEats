using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IFollowService
    {
        Task<IEnumerable<Follow>> GetUserFollowers(int userId);
        Task<IEnumerable<Follow>> GetUserFollowees(int userId);
        Task CreateFollow(int followerId, int followeeId);
    }
}
