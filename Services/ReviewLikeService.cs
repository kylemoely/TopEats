using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class ReviewLikeService : IReviewLikeService
    {
        private readonly IReviewLikeRepository _reviewLikeRepository;

        public ReviewLikeService(IReviewLikeRepository reviewLikeRepository)
        {
            _reviewLikeRepository = reviewLikeRepository;
        }

        public async Task<IEnumerable<ReviewLike>> GetReviewLikes(int reviewId)
        {
            return await _reviewLikeRepository.GetReviewLikes(reviewId);
        }

        public async Task CreateReviewLike(int reviewId, int userId);
        {
            await _reviewLikeRepository.CreateReviewLike(reviewId, userId);
        }
    }
}
