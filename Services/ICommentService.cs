using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface ICommentService
    {
        Task<Comment> GetCommentById(Guid commentId);
        Task<IEnumerable<Comment>> GetAllComments();
        Task CreateComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Guid commentId);
        Task<IEnumerable<Comment>> GetReviewComments(Guid reviewId);
    }
}