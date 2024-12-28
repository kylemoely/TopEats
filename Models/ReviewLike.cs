using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class ReviewLike
    {
        public Guid ReviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS

        [JsonIgnore]
        public UserDTO? AssignedUser { get; set; }
        [JsonIgnore]
        public Review? AssignedReview { get; set; }

        [JsonConstructor]
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