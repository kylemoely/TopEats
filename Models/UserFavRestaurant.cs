using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class UserFavRestaurant
    {
        public int userId { get; set; } // FOREIGN KEY REFERENCES USER
        public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS
        public int restaurantRank { get; set; }

        public User AssignedUser { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public UserFavRestaurant(int _userId, int _restaurantId, int _restaurantRank)
        {
            userId = _userId;
            restaurantId = _restaurantId;
            restaurantRank = _restaurantRank;
        }
        
        public async Task AssignUserAndRestaurant(IUserService userService, IRestaurantService restaurantService)
        {
            AssignedUser = await userService.GetUserById(userId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(restaurantId);
        }
    }
}