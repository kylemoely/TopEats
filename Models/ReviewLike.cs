using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class ReviewLike
    {
        public Guid ReviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS

        public UserDTO AssignedUser { get; set; }
        public Review AssignedReview { get; set; }

        public ReviewLike(Guid reviewId, Guid userId)
        {
            ReviewId = reviewId;
            UserId = userId;
        }

        public async Task AssignUserAndReview(IUserService userService, IReviewService reviewService)
        {
            AssignedUser = await userService.GetUserById(UserId);
            AssignedReview = await reviewService.GetReviewById(ReviewId);
        }
    }
}