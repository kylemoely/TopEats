using System;

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

        public Comment(int _commentId, int _reviewId, int _userId, string _commentText, IUserService userService, IReviewService reviewService)
        {
            commentId = _commentId;
            reviewId = _reviewId;
            userId = _userId;
            commentText = _commentText;

            AssignedReview = reviewService.GetReviewById(reviewId);
            AssignedUser = userService.GetUserById(userId);
        }

        public Comment(int _reviewId, int _userId, string _commentText, IUserService userService, IReviewService reviewService)
        {
            reviewId = _reviewId;
            userId = _userId;
            commentText = _commentText;

            AssignedReview = reviewService.GetReviewById(reviewId);
            AssignedUser = userService.GetUserById(userId);
        }
    }
}