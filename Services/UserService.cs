using System;
using System.Collections.Generic;
using TopEats.Models;
using TopEats.Repositories;

namespace TopEats.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}