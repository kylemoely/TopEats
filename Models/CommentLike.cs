using System;

namespace TopEats.Models
{

    public class CommentLike
    {
        public int commentId { get; set; } // FOREIGN KEY REFERENCES COMMENTS
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

        public Comment AssignedComment { get; set; } 
        public User AssignedUser { get; set; }

        public CommentLike(int _commentId, int _userId, IUserService userService, ICommentService commentService)
        {
            commentId = _commentId
            userId = _userId

            AssignedComment = commentService.GetCommentById(commentId)
            AssignedUser = userService.GetUserById(userId)
        }
    }

}