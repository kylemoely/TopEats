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
                command.Parameters.AddWithValue("@followeeId", userId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Follow follow = new Follow(
                            (int)reader["followerId"],
                            (int)reader["followeeId"]
                        );
                        await restaurant.AssignFollowerAndFollowee(_userService);
                        follows.Add(follow);
                    }
                }
            }

            return follows;
        }
    }
}
