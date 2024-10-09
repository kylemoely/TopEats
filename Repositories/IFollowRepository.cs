using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IFollowRepository
    {
        Task<IEnumerable<Follow>> GetUserFollowers(int userId);
    }
}
