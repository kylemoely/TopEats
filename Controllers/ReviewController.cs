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

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
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
                if (reviews.Any())
                {
                    return Ok(reviews);
                }
                return NotFound();
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
    }
}