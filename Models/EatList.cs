using System;
using TopEats.Services;
using System.Threading.Tasks;

namespace TopEats.Models
{
    public class EatList
    {
        public int? eatListId { get; set; } // PRIMARY KEY
        public string eatListName { get; set; }
        public bool private_setting { get; set; }
        public int userId { get; set; } // FOREIGN KEY REFERENCES USERS

        public User AssignedUser { get; set; }

        public EatList(int _eatListId, string _eatListName, bool _private_setting, int _userId)
        {
            eatListId = _eatListId;
            eatListName = _eatListName;
            private_setting = _private_setting;
            userId = _userId;
        }

        public EatList(string _eatListName, bool _private_setting, int _userId)
        {
            eatListName = _eatListName;
            private_setting = _private_setting;
            userId = _userId;
        }

        public async Task AssignUser(IUserService userService)
        {
            AssignedUser = await userService.GetUserById(userId);
        }
    }

}