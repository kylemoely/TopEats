using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class Comment
    {
        public Guid? CommentId { get; set; } // PRIMARY KEY
        public Guid ReviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS
        public string CommentText { get; set; } 

        [JsonIgnore]
        public Review? AssignedReview { get; set; }
        [JsonIgnore]
        public UserDTO? AssignedUser { get; set; }

        public Comment(Guid commentId, Guid reviewId, Guid userId, string commentText)
        {
            CommentId = commentId;
            ReviewId = reviewId;
            UserId = userId;
            CommentText = commentText;
        }
        [JsonConstructor]
        public Comment(Guid reviewId, Guid userId, string commentText)
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