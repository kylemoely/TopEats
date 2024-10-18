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
    public class EatListRestaurantRepository : IEatListRestaurantRepository
    {
        private readonly string _connectionString;
        private readonly IEatListService _eatListService;
        private readonly IRestaurantService _restaurantService;

        public EatListRestaurantRepository(IConfiguration configuration, IEatListService eatListService, IRestaurantService restaurantService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _eatListService = eatListService;
            _restaurantService = restaurantService;
        }

        public async Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(int eatListId)
        {
            List<EatListRestaurant> eatListRestaurants = new List<EatListRestaurant>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatListRestaurants WHERE eatListId = @eatListId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        EatListRestaurant eatListRestaurant = new EatListRestaurant(
                            (int)reader["eatListId"],
                            (int)reader["restaurantId"]
                        );
                        await eatListRestaurant.AssignEatListAndRestaurant(_eatListService, _restaurantService);
                        eatListRestaurants.Add(eatListRestaurant);
                    }
                }
            }

            return eatListRestaurants;
        }

        public async Task AddRestaurantToEatList(EatListRestaurant eatListRestaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO EatListRestaurants (eatListId, restaurantId) VALUES (@eatListId, @restaurantId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListRestaurant.eatListId);
                command.Parameters.AddWithValue("@restaurantId", eatListRestaurant.restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteRestaurantFromEatList(EatListRestaurant eatListRestaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM EatListRestaurants WHERE eatListId = @eatListId AND restaurantId = @restaurantId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListRestaurant.eatListId);
                command.Parameters.AddWithValue("@restaurantId", eatListRestaurant.restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
