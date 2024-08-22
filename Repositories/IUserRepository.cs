using System;
using System.Collections.Generic;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}