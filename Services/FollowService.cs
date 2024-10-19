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

        public async Task<IEnumerable<Follow>> GetUserFollowers(Guid userId)
        {
            return await _followRepository.GetUserFollowers(userId);
        }

        public async Task<IEnumerable<Follow>> GetUserFollowees(Guid userId)
        {
            return await _followRepository.GetUserFollowees(userId);
        }

        public async Task CreateFollow(Follow follow)
        {
            await _followRepository.CreateFollow(follow);
        }

        public async Task DeleteFollow(Follow follow)
        {
            await _followRepository.DeleteFollow(follow);
        }
    }
}
