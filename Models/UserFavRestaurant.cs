
public class UserFavRestaurant
{
    public int userId { get; set; } // FOREIGN KEY REFERENCES USER
    public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS
    public int restaurantRank { get; set; }

    public User AssignedUser { get; set; }
    public Restaurant AssignedRestaurant { get; set; }
}