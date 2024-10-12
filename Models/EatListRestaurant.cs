using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class EatListRestaurant
    {
        public int eatListId { get; set; } // FOREIGN KEY REFERENCES EATLISTS
        public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS

        public EatList AssignedEatList { get; set; }
        public Restaurant AssignedRestaurant { get; set; }

        public EatListRestaurant(int _eatListId, int _restaurantId)
        {
            eatListId = _eatListId;
            restaurantId = _restaurantId;
        }

        public async Task AssignEatListAndRestaurant(IEatListService eatListService, IRestaurantService restaurantService)
        {
            AssignedEatList = await eatListService.GetEatListById(eatListId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(restaurantId);
        }
    }
}