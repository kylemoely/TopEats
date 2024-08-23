using System;
using TopEats.Services;

namespace TopEats.Models
{
    public class ReviewLike
    {
        public int reviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

        public User AssignedUser { get; set; }
        public Review AssignedReview { get; set; }

        public ReviewLike(int _reviewId, int _userId, IUserService userService, IReviewService reviewService)
        {
            reviewId = _reviewId;
            userId = _userId;

            AssignedUser = userService.GetUserById(userId);
            AssignedReview = reviewService.GetReviewById(reviewId);
        }
    }
}