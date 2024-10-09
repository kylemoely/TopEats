using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> GetReviewById(int reviewId);
        Task<IEnumerable<Review>> GetAllReviews();
        Task CreateReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(int reviewId);
        Task<IEnumerable<Review>> GetUserReviews(int userId);
        Task<IEnumerable<Review>> GetRestaurantReviews(int restaurantId);
    }
}