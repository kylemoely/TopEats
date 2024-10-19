using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdatePassword(User user);
        Task DeleteUser(Guid userId);
    }
}