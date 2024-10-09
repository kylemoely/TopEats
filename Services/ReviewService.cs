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

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await _reviewRepository.GetReviewById(reviewId);
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _reviewRepository.GetAllReviews();
        }

        public async Task CreateReview(Review review)
        {
            await _reviewRepository.CreateReview(review);
        }

        public async Task UpdateReview(Review review)
        {
            await _reviewRepository.UpdateReview(review);
        }

        public async Task DeleteReview(int reviewId)
        {
            await _reviewRepository.DeleteReview(reviewId);
        }

        public async Task<IEnumerable<Review>> GetUserReviews(int userId)
        {
            return await _reviewRepository.GetUserReviews(userId);
        }

        public async Task<IEnumerable<Review>> GetRestaurantReviews(int restaurantId)
        {
            return await _reviewRepository.GetUserReviews(restaurantId);
        }

        public async Task<IEnumerable<Review>> GetFolloweeReviews(int userId)
        {
            return await _reviewRepository.GetFolloweeReviews(userId);
        }
    }
}
