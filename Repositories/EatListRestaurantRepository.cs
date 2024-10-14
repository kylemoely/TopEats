using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _eatListService = eatListService;
            _restaurantService = restaurantService;
        }

        public async Task<IEnumerable<EatListRestaurant>> GetEatListRestaurants(int eatListId)
        {
            List<EatListRestaurant> eatListRestaurants = new List<EatListRestaurant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM EatListRestaurants WHERE eatListId = @eatListId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public async Task AddRestaurantToEatList(int eatListId, int restaurantId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO EatListRestaurants (eatListId, restaurantId) VALUES (@eatListId, @restaurantId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListId);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteRestaurantFromEatList(EatListRestaurant eatListRestaurant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM EatListRestaurants WHERE eatListId = @eatListId AND restaurantId = @restaurantId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@eatListId", eatListRestaurant.eatListId);
                command.Parameters.AddWithValue("@restaurantId", eatListRestaurant.restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
