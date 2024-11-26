using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class Follow
    {
        public Guid FollowerId { get; set; } // FOREIGN KEY REFERENCES USERS
        public Guid FolloweeId { get; set; } // FOREIGN KEY REFERENCES USERS
        
        [JsonIgnore]
        public UserDTO? Follower { get; set; }
        [JsonIgnore]
        public UserDTO? Followee { get; set; }

        [JsonConstructor]
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