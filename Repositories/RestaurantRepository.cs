using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly string _connectionString;

        public RestaurantRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Restaurant> GetRestaurantById(int restaurantId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Restaurants WHERE restaurantId = @restaurantId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Restaurant
                        (
                            (int)reader["restaurantId"],
                            reader["restaurantName"].ToString(),
                            reader["cuisine"].ToString(),
                            (int)reader["priceCategory"]
                        );
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Restaurants";
                MySqlCommand command = new MySqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        restaurants.Add(new Restaurant
                        (
                            (int)reader["restaurantId"],
                            reader["restaurantName"].ToString(),
                            reader["cuisine"].ToString(),
                            (int)reader["priceCategory"]
                        ));
                    }
                }
            }
            return restaurants;
        }

        public async Task CreateRestaurant(Restaurant restaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Restaurants (restaurantName, cuisine, priceCategory) VALUES (@restaurantName, @cuisine, @priceCategory)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantName", restaurant.restaurantName);
                command.Parameters.AddWithValue("@cuisine", restaurant.cuisine);
                command.Parameters.AddWithValue("@priceCategory", restaurant.priceCategory);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateRestaurant(Restaurant restaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Restaurants SET restaurantName = @restaurantName, cuisine = @cuisine, priceCategory = @priceCategory WHERE restaurantId = @restaurantId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantName", restaurant.restaurantName);
                command.Parameters.AddWithValue("@cuisine", restaurant.cuisine);
                command.Parameters.AddWithValue("@priceCategory", restaurant.priceCategory);
                command.Parameters.AddWithValue("@restaurantId", restaurant.restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteRestaurant(int restaurantId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString)){
                string query = "DELETE FROM Restaurants WHERE restaurantId = @restaurantId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}