using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopEats.Models
{
    public class User
    {
        public int? userId { get; set; } // PRIMARY KEY
        public string username { get; set; }
        [JsonIgnore]
        public string passwordHash { get; set; }
        
        public User(int _userId, string _username, string _passwordHash)
        {
            userId = _userId;
            username = _username;
            passwordHash = _passwordHash;
        }

        public User(string _username, string _passwordHash)
        {
            username = _username;
            passwordHash = _passwordHash;
        }
    }
}