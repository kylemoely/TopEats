using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface ICommentLikeService
    {
        Task<CommentLike> GetCommentLikeById(CommentLike commentLike);
        Task<IEnumerable<CommentLike>> GetCommentLikes(Guid commentId);
        Task CreateCommentLike(CommentLike commentLike);
        Task DeleteCommentLike(CommentLike commentLike);
    }
}
