using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class ReviewLike
    {
        public int reviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

        public User AssignedUser { get; set; }
        public Review AssignedReview { get; set; }

        public ReviewLike(int _reviewId, int _userId)
        {
            reviewId = _reviewId;
            userId = _userId;
        }

        public async Task AssignUserAndReview(IUserService userService, IReviewService reviewService)
        {
            AssignedUser = await userService.GetUserById(userId);
            AssignedReview = await reviewService.GetReviewById(reviewId);
        }
    }
}