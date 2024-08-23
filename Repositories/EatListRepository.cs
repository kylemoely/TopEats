using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public EatList GetEatListById(int eatListId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new EatList
                        {
                            _eatListId = (int)reader["eatListId"],
                            _eatListName = reader["eatListName"].ToString(),
                            _private_setting = (bool)reader["private_setting"],
                            userService = _userService
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<EatList> GetAllEatLists()
        {
            List<EatList> eatLists = new List<EatList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatLists";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        eatLists.Add(new EatList
                        {
                            _eatListId = (int)reader["eatListId"],
                            _eatListname = reader["eatListName"].ToString(),
                            _private_setting = (bool)reader["private_setting"],
                            userService = _userService
                        });
                    }
                }
            }
            return EatLists;
        }

        public void CreateEatList(EatList eatList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO EatLists (eatListName, private_setting) VALUES (@eatListName, @private_setting)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListName", eatList.eatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.private_setting);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEatList(EatList eatList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE EatLists SET eatListName = @eatListName, private_setting = @private_setting WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListName", eatList.eatListName);
                command.Parameters.AddWithValue("@private_setting", eatList.private_setting);
                command.Parameters.AddWithValue("@eatListId", eatList.eatListId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEatList(int eatListId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM EatLists WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}