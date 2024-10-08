using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{

    public class CommentLike
    {
        public int commentId { get; set; } // FOREIGN KEY REFERENCES COMMENTS
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

        public Comment AssignedComment { get; set; } 
        public User AssignedUser { get; set; }

        public CommentLike(int _commentId, int _userId)
        {
            commentId = _commentId;
            userId = _userId;
        }

        public async Task AssignCommentAndUser(ICommentService commentService, IUserService userService)
        {
            AssignedComment = await commentService.GetCommentById(commentId);
            AssignedUser = await userService.GetUserById(userId);
        }
    }

}