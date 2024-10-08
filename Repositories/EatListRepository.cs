using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class EatListRepository : IEatListRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;

        public EatListRepository(IConfiguration configuration, IUserService userService)
        {
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
        }

        public async Task<EatList> GetEatListById(int eatListId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        EatList eatList = new EatList
                        (
                            (int)reader["eatListId"],
                            reader["eatListName"].ToString(),
                            (bool)reader["private_setting"],
                            (int)reader["userId"]
                        );
                        eatList.AssignUser(_userService);
                        return eatList;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<EatList>> GetAllEatLists()
        {
            List<EatList> eatLists = new List<EatList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        EatList eatList = new EatList
                        (
                            (int)reader["eatListId"],
                            reader["eatListName"].ToString(),
                            (bool)reader["private_setting"],
                            (int)reader["userId"]
                        );
                        eatList.AssignUser(_userService);
                        eatLists.Add(eatList);
                    }
                }
            }
            return eatLists;
        }

        public async Task CreateEatList(EatList eatList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO EatLists (eatListName, private_setting) VALUES (@eatListName, @private_setting)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListName", eatList.eatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.private_setting);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateEatList(EatList eatList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE EatLists SET eatListName = @eatListName, private_setting = @private_setting WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListName", eatList.eatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.private_setting);
                command.Parameters.AddWithValue("@eatListId", eatList.eatListId);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteEatList(int eatListId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM EatLists WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }
    }
}