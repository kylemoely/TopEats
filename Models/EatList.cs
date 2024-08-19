
public class EatList
{
    public int eatListId { get; set; } // PRIMARY KEY
    public string eatListName { get; set; }
    public bool private_setting { get; set; }
    public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

    public User AssignedUser { get; set; }
}