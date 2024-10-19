using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class User
    {
        public Guid? UserId { get; set; } // PRIMARY KEY
        public string Username { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        public User(Guid userId, string username, string passwordHash)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
        }

        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}