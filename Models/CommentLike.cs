
public class CommentLike
{
    public int commentId { get; set; } // FOREIGN KEY REFERENCES COMMENTS
    public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

    public Comment AssignedComment { get; set; } 
    public User AssignedUser { get; set; }
}