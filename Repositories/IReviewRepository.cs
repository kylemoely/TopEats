using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IReviewRepository
    {
        Review GetReviewById(int reviewId);
        IEnumerable<Review> GetAllReviews();
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int reviewId);
    }
}