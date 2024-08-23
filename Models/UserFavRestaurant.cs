using System;
using TopEats.Services;

namespace TopEats.Models
{
    public class UserFavRestaurant
    {
        public int userId { get; set; } // FOREIGN KEY REFERENCES USER
        public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS
        public int restaurantRank { get; set; }

        public User AssignedUser { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public UserFavRestaurant(int _userId, int _restaurantId, int _restaurantRank, IUserService userService, IRestaurantService restaurantService)
        {
            userId = _userId;
            restaurantId = _restaurantId;
            restaurantRank = _restaurantRank;

            AssignedUser = userService.GetUserById(userId);
            AssignedRestaurant = restaurantService.GetRestaurantById(restaurantId);
        }
    }
}