using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly ICommentLikeRepository _commentLikeRepository;

        public CommentLikeService(ICommentLikeRepository commentLikeRepository)
        {
            _commentLikeRepository = commentLikeRepository;
        }

        public async Task<IEnumerable<CommentLike>> GetCommentLikes(int commentId)
        {
            return await _commentLikeRepository.GetCommentLikes(commentId);
        }

        public async Task CreateCommentLike(CommentLike commentLike)
        {
            await _commentLikeRepository.CreateCommentLike(commentId, userId);
        }
    }
}
