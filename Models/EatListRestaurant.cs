using System;

namespace TopEats.Models
{
    public class EatListRestaurant
    {
        public int eatListId { get; set; } // FOREIGN KEY REFERENCES EATLISTS
        public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS

        public EatList AssignedEatList { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public EatListRestaurant(int _eatListId, int _restaurantId, IEatListService eatListService, IRestaurantService restaurantService)
        {
            eatListId = _eatListId;
            restaurantId = _restaurantId;

            AssignedEatList = eatListService.GetEatListById(eatListId);
            AssignedRestaurant = restaurantService.GetRestaurantById(restaurantId);
        }

    }
}