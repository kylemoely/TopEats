using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class UserDTO
    {
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        
        public UserDTO(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}