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
    public class ReviewLikeController : ControllerBase
    {
        private readonly IReviewLikeService _reviewLikeService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewLikeController(IReviewLikeService reviewLikeService, IReviewService reviewService, IUserService userService)
        {
            _reviewLikeService = reviewLikeService;
            _reviewService = reviewService;
            _userService = userService;
        }

        // GET: api/ReviewLike/{reviewId}
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<IEnumerable<ReviewLike>>> GetReviewLikes(Guid reviewId)
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

                IEnumerable<ReviewLike> reviewLikes = await _reviewLikeService.GetReviewLikes(reviewId);
                return Ok(reviewLikes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // POST: api/ReviewLike
        [HttpPost]
        public async Task<ActionResult> CreateReviewLike([FromBody] ReviewLike reviewLike)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Review review = await _reviewService.GetReviewById(reviewLike.ReviewId);
                UserDTO user = await _userService.GetUserById(reviewLike.UserId);

                if (review == null || user == null)
                {
                    return NotFound();
                }

                await _reviewLikeService.CreateReviewLike(reviewLike);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // DELETE: api/ReviewLike
        [HttpDelete]
        public async Task<ActionResult> DeleteReviewLike([FromBody] ReviewLike reviewLike)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ReviewLike checkReviewLike = await _reviewLikeService.GetReviewLikeById(reviewLike);

                if (checkReviewLike == null)
                {
                    return NotFound();
                }

                await _reviewLikeService.DeleteReviewLike(reviewLike);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}
