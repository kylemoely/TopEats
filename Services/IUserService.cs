using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetUserById(Guid userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> CreateUser(User user);
        Task UpdatePassword(User user);
        Task DeleteUser(Guid userId);
    }
}