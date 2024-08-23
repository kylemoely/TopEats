using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TopEats.Models;

namespace TopEats.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
        }

        public User GetUserById(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE userId = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        (
                            (int)reader["userId"],
                            reader["username"].ToString(),
                            reader["passwordHash"].ToString()
                        );
                    }
                }
            }
            return null;
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        (
                            (int)reader["userId"],
                            reader["username"].ToString(),
                            reader["passwordHash"].ToString()
                        ));
                    }
                }
            }
            return users;
        }

        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (username, passwordHash) VALUES (@username, @passwordHash)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@passwordHash", user.passwordHash);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET passwordHash = @passwordHash WHERE userId = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@passwordHash", user.passwordHash);
                command.Parameters.AddWithValue("@userId", user.userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Users WHERE userId = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}