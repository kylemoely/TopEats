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
    private readonly IReviewService _reviewService;

    public CommentController(ICommentService commentService, IReviewService reviewService)
    {
      _commentService = commentService;
      _reviewService = reviewService;
    }

    // GET: /api/[controller]/commentId
    [HttpGet("{commentId}")]
    public async Task<ActionResult<Comment>> GetCommentById(Guid commentId)
    {
      try
      {
        Comment comment = await _commentService.GetCommentById(commentId);

        if (comment == null)
        {
          return NotFound( new { message = "Comment not found." } );
        }

        return Ok(comment);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message });
      }

    }

    // GET: /api/[controller]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
    {
      try
      {
        var comments = await _commentService.GetAllComments();

        if (comments == null)
        {
          return NotFound( new { message = "No comments found." } );
        }

        return Ok(comments);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
      }
    }

    // POST: /api/[controller]
    [HttpPost]
    public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment reqComment)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest("Comment data is invalid.");
        }

        Comment resComment = await _commentService.CreateComment(reqComment);

        return CreatedAtAction(nameof(GetCommentById), new { commentId = resComment.CommentId }, resComment);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
      }
    }

    // PUT: /api/[controller]
    [HttpPut("{commentId}")]
    public async Task<ActionResult> UpdateComment([FromBody] Comment comment, Guid commentId)
    {
      try
      {
        if (commentId == null)
        {
          return BadRequest();
        }
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }
        Comment checkComment = await _commentService.GetCommentById(commentId);
        if (checkComment == null)
        {
          return NotFound();
        }
        comment.CommentId = commentId;
        await _commentService.UpdateComment(comment);

        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
      }
    }

    // DELETE: /api/[controller]/commentId
    [HttpDelete("{commentId}")]
    public async Task<ActionResult> DeleteComment(Guid commentId)
    {
      try
      {
        await _commentService.DeleteComment(commentId);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
      }
    }

    [HttpGet("review/{reviewId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetReviewComments(Guid reviewId)
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

        IEnumerable<Comment> comments = await _commentService.GetReviewComments(reviewId);
        return Ok(comments);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occured on our end. Try again later.", details=ex.Message});
      }
    }
  }
}
