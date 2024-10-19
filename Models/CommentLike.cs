using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{

    public class CommentLike
    {
        public Guid CommentId { get; set; } // FOREIGN KEY REFERENCES COMMENTS
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS

        public Comment AssignedComment { get; set; } 
        public User AssignedUser { get; set; }

        public CommentLike(Guid commentId, Guid userId)
        {
            CommentId = commentId;
            UserId = userId;
        }

        public async Task AssignCommentAndUser(ICommentService commentService, IUserService userService)
        {
            AssignedComment = await commentService.GetCommentById(CommentId);
            AssignedUser = await userService.GetUserById(UserId);
        }
    }

}