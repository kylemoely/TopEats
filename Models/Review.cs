using System;
using TopEats.Services;

namespace TopEats.Models
{
    public class Review
    {
        public int? reviewId { get; set; } // PRIMARY KEY
        public int rating { get; set; }
        public string reviewText { get; set; }

        public int resturantId { get; set; } // FOREIGN KEY REFERENCES Restaurants
        public int userId { get; set; } // FOREIGN KEY REFERENCES Users
        
        public User AssignedUser { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public Review(int _reviewId, int _rating, string _reviewText, int _restaurantId, int _userId, IUserService userService, IRestaurantService restaurantService)
        {
            reviewId = _reviewId;
            rating = _rating;
            reviewText = _reviewText;
            restaurantId = _restaurantId;
            userId = _userId;

            AssignedUser = userService.GetUserById(userId);
            AssignedRestaurant = restaurantService.GetRestaurantById(restaurantId);
        }

        public Review(int _rating, string _reviewText, int _restaurantId, int _userId, IUserService userService, IRestaurantService restaurantService)
        {
            rating = _rating;
            reviewText = _reviewText;
            restaurantId = _restaurantId;
            userId = _userId;

            AssignedUser = userService.GetUserById(userId);
            AssignedRestaurant = restaurantService.GetRestaurantById(restaurantId);
        }
    }
}