
public class Comment
{
    public int commentId { get; set; } // PRIMARY KEY
    public int reviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
    public int userId { get; set; } // FOREIGN KEY REFERENCES USERS
    public string commentText { get; set; } 

    public Review AssignedReview { get; set; }
    public User AssignedUser { get; set; }
}