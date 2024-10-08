using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class Follow
    {
        public int followerId { get; set; } // FOREIGN KEY REFERENCES USERS
        public int followeeId { get; set; } // FOREIGN KEY REFERENCES USERS
        
        public User Follower { get; set; }
        public User Followee { get; set; }

        public Follow(int _followerId, int _followeeId)
        {
            followerId = _followerId;
            followeeId = _followeeId;
        }

        public async Task AssignFollowerAndFollowee(IUserService userService)
        {
            Follower = await userService.GetUserById(followerId);
            Followee = await userService.GetUserById(followeeId);
        }
    }
}