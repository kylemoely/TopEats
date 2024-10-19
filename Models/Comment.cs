using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class Comment
    {
        public int CommentId { get; set; } // PRIMARY KEY
        public int ReviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public int UserId { get; set; } // FOREIGN KEY REFERENCES USERS
        public string CommentText { get; set; } 

        [ValidateNever]
        [JsonIgnore]
        public Review AssignedReview { get; set; }
        [ValidateNever]
        public User AssignedUser { get; set; }

        [JsonConstructor]
        public Comment(int commentId, int reviewId, int userId, string commentText)
        {
            CommentId = commentId;
            ReviewId = reviewId;
            UserId = userId;
            CommentText = commentText;
        }

        public Comment(int reviewId, int userId, string commentText)
        {
            ReviewId = reviewId;
            UserId = userId;
            CommentText = commentText;
        }

        public async Task AssignUserAndReview(IUserService userService, IReviewService reviewService)
        {
            AssignedReview = await reviewService.GetReviewById(ReviewId);
            AssignedUser = await userService.GetUserById(UserId);
        }
    }
}