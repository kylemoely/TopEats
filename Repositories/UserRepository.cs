using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserDTO> GetUserById(Guid userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserDTO
                        (
                            Guid.Parse(reader["userId"].ToString()),
                            reader["username"].ToString()
                        );
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users";
                MySqlCommand command = new MySqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new UserDTO
                        (
                            Guid.Parse(reader["userId"].ToString()),
                            reader["username"].ToString()
                        ));
                    }
                }
            }
            return users;
        }

        public async Task<UserDTO> CreateUser(User user)
        {
            Guid userId = Guid.NewGuid();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (userId, username, passwordHash) VALUES (@userId, @username, @passwordHash)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return new UserDTO(
                userId,
                user.Username
            );
        }

        public async Task UpdatePassword(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET passwordHash = @passwordHash WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@userId", user.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteUser(Guid userId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString)){
                string query = "DELETE FROM Users WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}