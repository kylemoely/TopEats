using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            return await _commentRepository.GetCommentById(commentId);
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _commentRepository.GetAllComments();
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            return await _commentRepository.CreateComment(comment);
        }

        public async Task UpdateComment(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
        }

        public async Task DeleteComment(Guid commentId)
        {
            await _commentRepository.DeleteComment(commentId);
        }

        public async Task<IEnumerable<Comment>> GetReviewComments(Guid reviewId)
        {
            return await _commentRepository.GetReviewComments(reviewId);
        }
    }
}