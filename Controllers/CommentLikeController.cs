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

        public CommentLikeController(ICommentLikeService commentLikeService)
        {
            _commentLikeService = commentLikeService;
        }

        // GET: api/CommentLike/{commentId}
        [HttpGet("{commentId}")]
        public async Task<ActionResult<IEnumerable<CommentLike>>> GetCommentLikes(int commentId)
        {
            var commentLikes = await _commentLikeService.GetCommentLikes(commentId);

            if (commentLikes == null)
            {
                return NotFound( new { message = "Comment not found."} );
            }

            return Ok(commentLikes);
        }

        // POST: api/CommentLike
        [HttpPost]
        public async Task<ActionResult> CreateCommentLike([FromBody] CommentLike commentLike)
        {
            if (commentLike == null)
            {
                return BadRequest("Request body is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("CommentLike data is invalid.");
            }

            await _commentLikeService.CreateCommentLike(commentLike);

            // Return a 201 Created response with the created comment like information
            return CreatedAtAction(nameof(GetCommentLikes), new { commentId = commentLike.commentId }, commentLike);
        }

        // DELETE: api/CommentLike
        [HttpDelete]
        public async Task<ActionResult> DeleteCommentLike([FromBody] CommentLike commentLike)
        {
            if (commentLike == null)
            {
                return BadRequest("Request body is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("CommentLike data is invalid.");
            }

            await _commentLikeService.DeleteCommentLike(commentLike);

            return NoContent(); // 204 No Content response for successful deletion
        }
    }
}
