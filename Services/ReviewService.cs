using System;
using System.Collections.Generic;
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

        public Review GetReviewById(int reviewId)
        {
            return _reviewRepository.GetReviewById(reviewId);
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public void CreateReview(Review review)
        {
            _reviewRepository.CreateReview(review);
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
        }

        public void DeleteReview(int reviewId)
        {
            _reviewRepository.DeleteReview(reviewId);
        }
    }
}