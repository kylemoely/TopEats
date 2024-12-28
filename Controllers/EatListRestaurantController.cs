using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EatListRestaurantController : ControllerBase
    {
        private readonly IEatListRestaurantService _eatListRestaurantService;
        private readonly IEatListService _eatListService;
        private readonly IRestaurantService _restaurantService;

        public EatListRestaurantController(IEatListRestaurantService eatListRestaurantService, IEatListService eatListService, IRestaurantService restaurantService)
        {
            _eatListRestaurantService = eatListRestaurantService;
            _eatListService = eatListService;
            _restaurantService = restaurantService;
        }

        [HttpGet("{eatListId}")]
        public async Task<ActionResult<IEnumerable<EatListRestaurant>>> GetEatListRestaurants(Guid eatListId)
        {
            try
            {
                if (eatListId == null)
                {
                    return BadRequest();
                }

                EatList eatList = await _eatListService.GetEatListById(eatListId);

                if (eatList == null)
                {
                    return NotFound();
                }

                IEnumerable<EatListRestaurant> eatListRestaurants = await _eatListRestaurantService.GetEatListRestaurants(eatListId);

                return Ok(eatListRestaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddRestaurantToEatList(EatListRestaurant eatListRestaurant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                EatList eatList = await _eatListService.GetEatListById(eatListRestaurant.EatListId);
                Restaurant restaurant = await _restaurantService.GetRestaurantById(eatListRestaurant.RestaurantId);

                if (eatList == null || restaurant == null)
                {
                    return NotFound();
                }

                await _eatListRestaurantService.AddRestaurantToEatList(eatListRestaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRestaurantFromEatList(EatListRestaurant eatListRestaurant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                EatList eatList = await _eatListService.GetEatListById(eatListRestaurant.EatListId);
                Restaurant restaurant = await _restaurantService.GetRestaurantById(eatListRestaurant.RestaurantId);

                if (eatList == null || restaurant == null)
                {
                    return NotFound();
                }

                await _eatListRestaurantService.DeleteRestaurantFromEatList(eatListRestaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}