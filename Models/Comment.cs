using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class Comment
    {
        public int? commentId { get; set; } // PRIMARY KEY
        public int reviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS
        public string commentText { get; set; } 

        public Review AssignedReview { get; set; }
        public User AssignedUser { get; set; }

        public Comment(int _commentId, int _reviewId, int _userId, string _commentText)
        {
            commentId = _commentId;
            reviewId = _reviewId;
            userId = _userId;
            commentText = _commentText;
        }

        public Comment(int _reviewId, int _userId, string _commentText)
        {
            reviewId = _reviewId;
            userId = _userId;
            commentText = _commentText;
        }

        public async Task AssignUserAndReview(IUserService userService, IReviewService reviewService)
        {
            AssignedReview = await reviewService.GetReviewById(reviewId);
            AssignedUser = await userService.GetUserById(userId);
        }
    }
}