using System;
using System.Collections.Generic;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository
        }

        public Comment GetCommentById(int commentId)
        {
            return _commentRepository.GetCommentById(commentId);
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _commentRepository.GetAllComments();
        }

        public void CreateComment(Comment comment)
        {
            return _commentRepository.CreateComment(comment);
        }

        public void UpdateComment(Comment comment)
        {
            return _commentRepository.UpdateComment(comment);
        }

        public void DeleteComment(int commentId)
        {
            return _commentRepository.DeleteComment(commentId);
        }
    }
}