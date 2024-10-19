using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;

        public FollowRepository(IConfiguration configuration, IUserService userService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
        }

        public async Task<IEnumerable<Follow>> GetUserFollowers(Guid userId)
        {
            List<Follow> followers = new List<Follow>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Follows WHERE followeeId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Follow follower = new Follow(
                            Guid.Parse(reader["followerId"].ToString()),
                            Guid.Parse(reader["followeeId"].ToString())
                        );
                        await follower.AssignFollowerAndFollowee(_userService);
                        followers.Add(follower);
                    }
                }
            }

            return followers;
        }

        public async Task<IEnumerable<Follow>> GetUserFollowees(Guid userId)
        {
            List<Follow> followees = new List<Follow>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Follows WHERE followerId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Follow followee = new Follow(
                            Guid.Parse(reader["followerId"].ToString()),
                            Guid.Parse(reader["followeeId"].ToString())
                        );
                        await followee.AssignFollowerAndFollowee(_userService);
                        followees.Add(followee);
                    }
                }
            }
            return followees;
        }

        public async Task CreateFollow(Follow follow)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Follows (followerId, followeeId) VALUES (@followerId, @followeeId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@followerId", follow.FollowerId);
                command.Parameters.AddWithValue("@followeeId", follow.FolloweeId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteFollow(Follow follow)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM Follows WHERE followerId = @followerId AND followeeId = @followeeId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@followerId", follow.FollowerId);
                command.Parameters.AddWithValue("@followeeId", follow.FolloweeId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
