using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class Review
    {
        public Guid? ReviewId { get; set; } // PRIMARY KEY
        public int Rating { get; set; }
        public string ReviewText { get; set; }

        public Guid RestaurantId { get; set; } // FOREIGN KEY REFERENCES Restaurants
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES Users
        
        [JsonIgnore]
        public UserDTO? AssignedUser { get; set; }
        [JsonIgnore]
        public Restaurant? AssignedRestaurant { get; set; }

        public Review(Guid reviewId, int rating, string reviewText, Guid restaurantId, Guid userId)
        {
            ReviewId = reviewId;
            Rating = rating;
            ReviewText = reviewText;
            RestaurantId = restaurantId;
            UserId = userId;
        }
        [JsonConstructor]
        public Review(int rating, string reviewText, Guid restaurantId, Guid userId)
        {
            Rating = rating;
            ReviewText = reviewText;
            RestaurantId = restaurantId;
            UserId = userId;
        }

        public async Task AssignUserAndRestaurant(IUserService userService, IRestaurantService restaurantService)
        {
            AssignedUser = await userService.GetUserById(UserId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(RestaurantId);
        }
    }
}