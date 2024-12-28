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

        public async Task<ReviewLike> GetReviewLikeById(ReviewLike reviewLike)
        {
            return await _reviewLikeRepository.GetReviewLikeById(reviewLike);
        }

        public async Task<IEnumerable<ReviewLike>> GetReviewLikes(Guid reviewId)
        {
            return await _reviewLikeRepository.GetReviewLikes(reviewId);
        }

        public async Task CreateReviewLike(ReviewLike reviewLike)
        {
            await _reviewLikeRepository.CreateReviewLike(reviewLike);
        }

        public async Task DeleteReviewLike(ReviewLike reviewLike)
        {
            await _reviewLikeRepository.DeleteReviewLike(reviewLike);
        }
    }
}
