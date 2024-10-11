using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
        }

        public async Task<IEnumerable<Follow>> GetUserFollowers(int userId)
        {
            List<Follow> followers = new List<Follow>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Follows WHERE followeeId = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Follow follower = new Follow(
                            (int)reader["followerId"],
                            (int)reader["followeeId"]
                        );
                        await follower.AssignFollowerAndFollowee(_userService);
                        followers.Add(follower);
                    }
                }
            }

            return followers;
        }

        public async Task<IEnumerable<Follow>> GetUserFollowees(int userId)
        {
            List<Follow> followees = new List<Follow>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Follows WHERE followerId = @userId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Follow followee = new Follow(
                            (int)reader["followerId"],
                            (int)reader["followeeId"]
                        );
                        await followee.AssignFollowerAndFollowee(_userService);
                        followees.Add(followee);
                    }
                }
            }
            return followees;
        }

        public async Task CreateFollow(int followerId, int followeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Follows (followerId, followeeId) VALUES (@followerId, @followeeId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@followerId", followerId);
                command.Parameters.AddWithValue("@followeeId", followeeId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
