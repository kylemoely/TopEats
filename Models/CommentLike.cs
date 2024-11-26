using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{

    public class CommentLike
    {
        public Guid CommentId { get; set; } // FOREIGN KEY REFERENCES COMMENTS
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS

        [JsonIgnore]
        public Comment? AssignedComment { get; set; }
        [JsonIgnore] 
        public UserDTO? AssignedUser { get; set; }

        [JsonConstructor]
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