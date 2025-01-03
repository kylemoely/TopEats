using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class EatListRestaurant
    {
        public Guid EatListId { get; set; } // FOREIGN KEY REFERENCES EATLISTS
        public Guid RestaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS

        [JsonIgnore]
        public EatList? AssignedEatList { get; set; }
        [JsonIgnore]
        public Restaurant? AssignedRestaurant { get; set; }
        
        [JsonConstructor]
        public EatListRestaurant(Guid eatListId, Guid restaurantId)
        {
            EatListId = eatListId;
            RestaurantId = restaurantId;
        }

        public async Task AssignEatListAndRestaurant(IEatListService eatListService, IRestaurantService restaurantService)
        {
            AssignedEatList = await eatListService.GetEatListById(EatListId);
            AssignedRestaurant = await restaurantService.GetRestaurantById(RestaurantId);
        }
    }
}