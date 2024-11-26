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
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IUserService _userService;

        public FollowController(IFollowService followService, IUserService userService)
        {
            _followService = followService;
            _userService = userService;
        }

        [HttpGet("follower/{userId}")]
        public async Task<ActionResult<IEnumerable<Follow>>> GetUserFollowers(Guid userId)
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

                IEnumerable<Follow> followers = await _followService.GetUserFollowers(userId);
                return Ok(followers);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpGet("followee/{userId}")]
        public async Task<ActionResult<IEnumerable<Follow>>> GetUserFollowees(Guid userId)
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

                IEnumerable<Follow> followees = await _followService.GetUserFollowees(userId);
                return Ok(followees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFollow([FromBody] Follow follow)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserDTO user1 = await _userService.GetUserById(follow.FollowerId);
                UserDTO user2 = await _userService.GetUserById(follow.FolloweeId);

                if (user1 == null || user2 == null)
                {
                    return NotFound();
                }

                await _followService.CreateFollow(follow);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}