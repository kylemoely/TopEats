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

                IEnumerable<Follow> follows = await _followService.GetUserFollowers(userId);
                return Ok(follows);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}