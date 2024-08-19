
public class Follow
{
    public int followerId { get; set; } // FOREIGN KEY REFERENCES USERS
    public int followeeId { get; set; } // FOREIGN KEY REFERENCES USERS
    
    public User Follower { get; set; }
    public User Followee { get; set; }
}