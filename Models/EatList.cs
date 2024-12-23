using System;
using TopEats.Services;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class EatList
    {
        public Guid? EatListId { get; set; } // PRIMARY KEY
        public string EatListName { get; set; }
        public bool Private_setting { get; set; }
        public Guid UserId { get; set; } // FOREIGN KEY REFERENCES USERS

        [JsonIgnore]
        public UserDTO? AssignedUser { get; set; }

        public EatList(Guid eatListId, string eatListName, bool private_setting, Guid userId)
        {
            EatListId = eatListId;
            EatListName = eatListName;
            Private_setting = private_setting;
            UserId = userId;
        }
        [JsonConstructor]
        public EatList(string eatListName, bool private_setting, Guid userId)
        {
            EatListName = eatListName;
            Private_setting = private_setting;
            UserId = userId;
        }

        public async Task AssignUser(IUserService userService)
        {
            AssignedUser = await userService.GetUserById(UserId);
        }
    }

}