using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface ICommentLikeRepository
    {
        Task<IEnumerable<CommentLike>> GetCommentLikes(int commentId);
        Task CreateCommentLike(int commentId, int userId);
    }
}
