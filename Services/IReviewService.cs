using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IReviewService
    {
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetAllReviews();
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int reviewId);
    }
}