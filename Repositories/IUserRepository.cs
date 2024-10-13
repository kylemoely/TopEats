using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdatePassword(User user);
        Task DeleteUser(int userId);
    }
}