using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Services
{
    public interface ICommentService
    {
        Comment GetCommentById(int commentId);
        IEnumerable<Comment> GetAllComments();
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int commentId);
    }
}