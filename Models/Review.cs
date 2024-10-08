using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class Review
    {
        public int? reviewId { get; set; } // PRIMARY KEY
        public int rating { get; set; }
        public string reviewText { get; set; }

        public int restaurantId { get; set; } // FOREIGN KEY REFERENCES Restaurants
        public int userId { get; set; } // FOREIGN KEY REFERENCES Users
        
        public User AssignedUser { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public Review(int _reviewId, int _rating, string _reviewText, int _restaurantId, int _userId)
        {
            reviewId = _reviewId;
            rating = _rating;
            reviewText = _reviewText;
            restaurantId = _restaurantId;
            userId = _userId;
        }

        public Review(int _rating, string _reviewText, int _restaurantId, int _userId)
        {
            rating = _rating;
            reviewText = _reviewText;
            restaurantId = _restaurantId;
            userId = _userId;
        }

        public async Task AssignUserAndRestaurant(IUserService userService, IRestaurantService restaurantService)
        {
            AssignedUser = await userService.GetUserById(userId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(restaurantId);
        }
    }
}