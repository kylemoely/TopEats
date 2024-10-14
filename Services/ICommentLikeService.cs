using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface ICommentLikeService
    {
        Task<IEnumerable<CommentLike>> GetCommentLikes(int commentId);
        Task CreateCommentLike(CommentLike commentLike);
    }
}
