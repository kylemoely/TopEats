using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<IEnumerable<Follow>> GetUserFollowers(int userId)
        {
            return await _followRepository.GetUserFollowers(userId);
        }

        public async Task<IEnumerable<Follow>> GetUserFollowees(int userId)
        {
            return await _followRepository.GetUserFollowees(userId);
        }

        public async Task CreateFollow(int followerId, int followeeId)
        {
            await _followRepository.CreateFollow(followerId, followeeId);
        }
    }
}
