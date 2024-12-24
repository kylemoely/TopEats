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
    public class EatListRepository : IEatListRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;

        public EatListRepository(IConfiguration configuration, IUserService userService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
        }

        public async Task<EatList> GetEatListById(Guid eatListId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists WHERE eatListId = @eatListId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        EatList eatList = new EatList
                        (
                            Guid.Parse(reader["eatListId"].ToString()),
                            reader["eatListName"].ToString(),
                            (bool)reader["private_setting"],
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await eatList.AssignUser(_userService);
                        return eatList;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<EatList>> GetAllEatLists()
        {
            List<EatList> eatLists = new List<EatList>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists";
                MySqlCommand command = new MySqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        EatList eatList = new EatList
                        (
                            Guid.Parse(reader["eatListId"].ToString()),
                            reader["eatListName"].ToString(),
                            (bool)reader["private_setting"],
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await eatList.AssignUser(_userService);
                        eatLists.Add(eatList);
                    }
                }
            }
            return eatLists;
        }

        public async Task<EatList> CreateEatList(EatList eatList)
        {
            Guid eatListId = Guid.NewGuid();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO EatLists (eatListId, eatListName, private_setting, userId) VALUES (@eatListId, @eatListName, @private_setting, @userId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);
                command.Parameters.AddWithValue("@eatListName", eatList.EatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.Private_setting);
                command.Parameters.AddWithValue("@userId", eatList.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                eatList.EatListId = eatListId;

                return eatList;
            }
        }

        public async Task UpdateEatList(EatList eatList)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE EatLists SET eatListName = @eatListName, private_setting = @private_setting WHERE eatListId = @eatListId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListName", eatList.EatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.Private_setting);
                command.Parameters.AddWithValue("@eatListId", eatList.EatListId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteEatList(Guid eatListId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString)){
                string query = "DELETE FROM EatLists WHERE eatListId = @eatListId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<EatList>> GetUserEatLists(Guid userId)
        {
            List<EatList> eatLists = new List<EatList>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        EatList eatList = new EatList
                        (
                            Guid.Parse(reader["eatListId"].ToString()),
                            reader["eatListName"].ToString(),
                            (bool)reader["private_setting"],
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await eatList.AssignUser(_userService);
                        eatLists.Add(eatList);
                    }
                }
            }
            return eatLists;
        }
    }
}
