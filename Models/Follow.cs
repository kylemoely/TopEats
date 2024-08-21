using System;

namespace TopEats.Models
{
    public class Follow
    {
        public int followerId { get; set; } // FOREIGN KEY REFERENCES USERS
        public int followeeId { get; set; } // FOREIGN KEY REFERENCES USERS
        
        public User Follower { get; set; }
        public User Followee { get; set; }

        public Follow(int _followerId, int _followeeId, IUserService userService)
        {
            followerId = _followerId
            followeeId = _followeeId

            Follower = userService.GetUserById(followerId)
            Followee = userService.GetUserById(followeeId)
        }
    }
}