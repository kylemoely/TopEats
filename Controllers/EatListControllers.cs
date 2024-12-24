using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EatListController : ControllerBase
    {
        private readonly IEatListService _eatListService;
        private readonly IUserService _userService;

        public EatListController(IEatListService eatListService, IUserService userService)
        {
            _eatListService = eatListService;
            _userService = userService;
        }

        [HttpGet("{eatListId}")]
        public async Task<ActionResult<EatList>> GetEatListById(Guid eatListId)
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

                return Ok(eatList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<EatList>> CreateEatList([FromBody] EatList reqEatList)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserDTO user = await _userService.GetUserById(reqEatList.UserId);

                if (user == null)
                {
                    return NotFound();
                }

                EatList resEatList = await _eatListService.CreateEatList(reqEatList);

                return CreatedAtAction(nameof(GetEatListById), new { eatListId = resEatList.EatListId }, resEatList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpPut("{eatListId}")]
        public async Task<IActionResult> UpdateEatList ([FromBody] EatList eatList, Guid eatListId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                EatList checkEatList = await _eatListService.GetEatListById(eatListId);

                if (checkEatList == null)
                {
                    return NotFound();
                }

                eatList.EatListId = eatListId;
                await _eatListService.UpdateEatList(eatList);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        [HttpDelete("{eatListId}")]
        public async Task<IActionResult> DeleteEatList(Guid eatListId)
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

                await _eatListService.DeleteEatList(eatListId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}