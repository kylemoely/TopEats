using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantById (Guid restaurantId)
        {
            try
            {
                Console.WriteLine(restaurantId);
                Restaurant restaurant = await _restaurantService.GetRestaurantById(restaurantId);

                if (restaurant == null)
                {
                    return NotFound();
                }

                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
        {
            try
            {
                IEnumerable<Restaurant> restaurants = await _restaurantService.GetAllRestaurants();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] Restaurant reqRestaurant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Restaurant resRestaurant = await _restaurantService.CreateRestaurant(reqRestaurant);

                return CreatedAtAction(nameof(GetRestaurantById), new { id = resRestaurant.RestaurantId }, resRestaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPut("{restaurantId}")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] Restaurant reqRestaurant, Guid restaurantId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Restaurant checkRestaurant = await _restaurantService.GetRestaurantById(restaurantId);
                if (checkRestaurant == null)
                {
                    return NotFound();
                }
                await _restaurantService.UpdateRestaurant(reqRestaurant);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpDelete("{restaurantId}")]
        public async Task<IActionResult> DeleteRestaurant(Guid restaurantId)
        {
            try
            {
                if (restaurantId == null)
                {
                    return BadRequest();
                }
                Restaurant restaurant = await _restaurantService.GetRestaurantById(restaurantId);
                if (restaurant == null)
                {
                    return NotFound();
                }

                await _restaurantService.DeleteRestaurant(restaurantId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}