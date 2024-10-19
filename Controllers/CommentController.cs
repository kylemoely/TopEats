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
  public class CommentController : ControllerBase
  {
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
      _commentService = commentService;
    }

    // GET: /api/[controller]/commentId
    [HttpGet("{commentId}")]
    public async Task<ActionResult<Comment>> GetCommentById(Guid commentId)
    {
      var comment = await _commentService.GetCommentById(commentId);

      if (comment == null)
      {
        return NotFound( new { message = "Comment not found." } );
      }

      return Ok(comment);
    }

    // GET: /api/[controller]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
    {
      var comments = await _commentService.GetAllComments();

      if (comments == null)
      {
        return NotFound( new { message = "No comments found." } );
      }

      return Ok(comments);
    }

    // POST: /api/[controller]
    [HttpPost]
    public async Task<ActionResult> CreateComment([FromBody] Comment comment)
    {
      if (comment == null)
      {
        return BadRequest("Request body is null.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest("Comment data is invalid.");
      }

      await _commentService.CreateComment(comment);

      return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment);
    }

    // PUT: /api/[controller]
    [HttpPut]
    public async Task<ActionResult> UpdateComment([FromBody] Comment comment)
    {
      if (comment == null)
      {
        return BadRequest("Request body is null.");
      }
      if (!ModelState.IsValid)
      {
        return BadRequest("Comment data is invalid.");
      }

      await _commentService.UpdateComment(comment);

      return NoContent();
    }

    // DELETE: /api/[controller]/commentId
    // [HttpDelete("{commentId}")]
    // public async Task<ActionResult> DeleteComment(int commentId)
    // {
      
    // }
  }
}
