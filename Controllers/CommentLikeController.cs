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
    public class CommentLikeController : ControllerBase
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentLikeController(ICommentLikeService commentLikeService, ICommentService commentService, IUserService userService)
        {
            _commentLikeService = commentLikeService;
            _commentService = commentService;
            _userService = userService;
        }

        // GET: api/CommentLike/{commentId}
        [HttpGet("{commentId}")]
        public async Task<ActionResult<IEnumerable<CommentLike>>> GetCommentLikes(Guid commentId)
        {
            try
            {
                if (commentId == null)
                {
                    return BadRequest();
                }

                Comment comment = await _commentService.GetCommentById(commentId);
                if (comment == null)
                {
                    return NotFound();
                }

                IEnumerable<CommentLike> commentLikes = await _commentLikeService.GetCommentLikes(commentId);
                return Ok(commentLikes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // POST: api/CommentLike
        [HttpPost]
        public async Task<ActionResult> CreateCommentLike([FromBody] CommentLike commentLike)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Comment comment = await _commentService.GetCommentById(commentLike.CommentId);
                UserDTO user = await _userService.GetUserById(commentLike.UserId);

                if (comment == null || user == null)
                {
                    return NotFound();
                }

                await _commentLikeService.CreateCommentLike(commentLike);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }

        // DELETE: api/CommentLike
        [HttpDelete]
        public async Task<ActionResult> DeleteCommentLike([FromBody] CommentLike commentLike)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                CommentLike checkCommentLike = await _commentLikeService.GetCommentLikeById(commentLike);

                if (checkCommentLike == null)
                {
                    return NotFound();
                }

                await _commentLikeService.DeleteCommentLike(commentLike);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
            }
        }
    }
}
