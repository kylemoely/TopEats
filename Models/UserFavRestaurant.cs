using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class UserFavRestaurant
    {
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USER
        public Guid RestaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS
        public int RestaurantRank { get; set; }

        [JsonIgnore]
        public UserDTO? AssignedUser { get; set; }
        [JsonIgnore]
        public Restaurant? AssignedRestaurant { get; set; }

        [JsonConstructor]
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