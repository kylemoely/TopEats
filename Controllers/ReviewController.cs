using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IRestaurantService _restaurantService;

        public ReviewController(IReviewService reviewService, IUserService userService, IRestaurantService restaurantService)
        {
            _reviewService = reviewService;
            _userService = userService;
            _restaurantService = restaurantService;
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReviewById(Guid reviewId)
        {
            try
            {
                if (reviewId == null)
                {
                    return BadRequest();
                }

                Review review = await _reviewService.GetReviewById(reviewId);

                if (review == null)
                {
                    return NotFound();
                }

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            try
            {
                IEnumerable<Review> reviews = await _reviewService.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review reqReview)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Review resReview = await _reviewService.CreateReview(reqReview);

                return CreatedAtAction(nameof(GetReviewById), new { reviewId = resReview.ReviewId }, resReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview([FromBody] Review review, Guid reviewId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Review checkReview = await _reviewService.GetReviewById(reviewId);

                if (checkReview == null)
                {
                    return NotFound();
                }

                review.ReviewId = reviewId;
                await _reviewService.UpdateReview(review);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            try
            {
                if (reviewId == null)
                {
                    return BadRequest();
                }

                Review review = await _reviewService.GetReviewById(reviewId);

                if (review == null)
                {
                    return NotFound();
                }

                await _reviewService.DeleteReview(reviewId);

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetUserReviews(Guid userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest();
                }
                
                UserDTO user = await _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                IEnumerable<Review> reviews = await _reviewService.GetUserReviews(userId);
                return Ok(reviews);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetRestaurantReviews(Guid restaurantId)
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

                IEnumerable<Review> reviews = await _reviewService.GetRestaurantReviews(restaurantId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}