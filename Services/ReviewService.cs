using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> GetReviewById(Guid reviewId)
        {
            return await _reviewRepository.GetReviewById(reviewId);
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _reviewRepository.GetAllReviews();
        }

        public async Task<Review> CreateReview(Review review)
        {
            return await _reviewRepository.CreateReview(review);
        }

        public async Task UpdateReview(Review review)
        {
            await _reviewRepository.UpdateReview(review);
        }

        public async Task DeleteReview(Guid reviewId)
        {
            await _reviewRepository.DeleteReview(reviewId);
        }

        public async Task<IEnumerable<Review>> GetUserReviews(Guid userId)
        {
            return await _reviewRepository.GetUserReviews(userId);
        }

        public async Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId)
        {
            return await _reviewRepository.GetUserReviews(restaurantId);
        }

        public async Task<IEnumerable<Review>> GetFolloweeReviews(Guid userId)
        {
            return await _reviewRepository.GetFolloweeReviews(userId);
        }
    }
}
