
public class EatListRestaurant
{
    public int eatListId { get; set; } // FOREIGN KEY REFERENCES EATLISTS
    public int restaurantId { get; set; } // FOREIGN KEY REFERENCES RESTAURANTS

    public EatList AssignedEatList { get; set; }
    public Restaurant Assigned Restaurant { get; set; }
}