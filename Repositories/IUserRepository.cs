using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserById(Guid userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdatePassword(User user);
        Task DeleteUser(Guid userId);
    }
}