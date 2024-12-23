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
        public async Task<ActionResult<EatList>> CreateEatList([FromBody] EatList eatList)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserDTO user = await _userService.GetUserById(eatList.UserId);

                if (user == null)
                {
                    return NotFound();
                }
            }
        }
    }
}