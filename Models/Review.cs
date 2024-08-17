
public class Review
{
    public int reviewId { get; set; } // PRIMARY KEY
    public int rating { get; set; }
    public string reviewText { get; set; }

    public int resturantId { get; set; } // FOREIGN KEY REFERENCES Restaurants
    public int userId { get; set; } // FOREIGN KEY REFERENCES Users
    
    public User AssignedUser { get; set; }
    public Restaurant AssignedRestaurant { get; set; }
}