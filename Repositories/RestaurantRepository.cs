using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Restaurant GetRestaurantById(int restaurantId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Restaurants WHERE restaurantId = @restaurantId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Restaurant
                        {
                            _restaurantId = (int)reader["restaurantId"],
                            _restaurantName = reader["restaurantName"].ToString(),
                            _cuisine = reader["cuisine"].ToString(),
                            _priceCategory = (int)reader["priceCategory"]
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Restaurants";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        restaurants.Add(new Restaurant
                        {
                            _restaurantId = (int)reader["restaurantId"],
                            _restaurantName = reader["restaurantName"].ToString(),
                            _cuisine = reader["cuisine"].ToString(),
                            _priceCategory = (int)reader["priceCategory"]
                        });
                    }
                }
            }
            return restaurants;
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Restaurants (restaurantName, cuisine, priceCategory) VALUES (@restaurantName, @cuisine, @priceCategory)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantName", restaurant.restaurantName);
                command.Parameters.AddWithValue("@cuisine", restaurant.cuisine);
                command.Parameters.AddWithValue("@priceCategory", restaurant.priceCategory);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Restaurants SET restaurantName = @restaurantName, cuisine = @cuisine, priceCategory = @priceCategory WHERE restaurantId = @restaurantId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantName", restaurant.restaurantName);
                command.Parameters.AddWithValue("@cuisine", restaurant.cuisine);
                command.Parameters.AddWithValue("@priceCategory", restaurant.priceCategory);
                command.Parameters.AddWithValue("@restaurantId", restaurant.restaurantId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteRestaurant(int restaurantId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Restaurants WHERE restaurantId = @restaurantId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}