using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class Follow
    {
        public Guid FollowerId { get; set; } // FOREIGN KEY REFERENCES USERS
        public Guid FolloweeId { get; set; } // FOREIGN KEY REFERENCES USERS
        
        public User Follower { get; set; }
        public User Followee { get; set; }

        public Follow(Guid followerId, Guid followeeId)
        {
            FollowerId = followerId;
            FolloweeId = followeeId;
        }

        public async Task AssignFollowerAndFollowee(IUserService userService)
        {
            Follower = await userService.GetUserById(FollowerId);
            Followee = await userService.GetUserById(FolloweeId);
        }
    }
}