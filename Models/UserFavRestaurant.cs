using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class UserFavRestaurant
    {
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USER
        public Guid RestaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS
        public int RestaurantRank { get; set; }

        public User AssignedUser { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public UserFavRestaurant(Guid userId, Guid restaurantId, int restaurantRank)
        {
            UserId = userId;
            RestaurantId = restaurantId;
            RestaurantRank = restaurantRank;
        }
        
        public async Task AssignUserAndRestaurant(IUserService userService, IRestaurantService restaurantService)
        {
            AssignedUser = await userService.GetUserById(UserId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(RestaurantId);
        }
    }
}