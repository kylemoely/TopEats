
public class ReviewLike
{
    public int reviewId { get; set; } // FOREIGN KEY REFERENCES REVIEWS
    public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

    public User AssignedUser { get; set; }
    public Review AssignedReview { get; set; }
}