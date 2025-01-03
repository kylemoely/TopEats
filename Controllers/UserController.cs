using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                IEnumerable<UserDTO> users = await _userService.GetAllUsers();
                return Ok(users);                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }

        }

        // GET: api/User/{id}
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid userId)
        {
            try
            {
                UserDTO user = await _userService.GetUserById(userId);
                
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] User newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                UserDTO userDTO = await _userService.CreateUser(newUser);

                return CreatedAtAction(nameof(GetUserById), new { userId = userDTO.UserId }, userDTO);                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdatePassword([FromBody] User updatedUser, Guid userId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                UserDTO user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }
                updatedUser.UserId = userId;
                await _userService.UpdatePassword(updatedUser);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // DELETE: api/User/{id}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                UserDTO user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteUser(userId);

                return NoContent();                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}
