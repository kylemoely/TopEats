using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IReviewService
    {
        Task<Review> GetReviewById(Guid reviewId);
        Task<IEnumerable<Review>> GetAllReviews();
        Task CreateReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(Guid reviewId);
        Task<IEnumerable<Review>> GetUserReviews(Guid userId);
        Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId);
        Task<IEnumerable<Review>> GetFolloweeReviews(Guid userId);
    }
}
