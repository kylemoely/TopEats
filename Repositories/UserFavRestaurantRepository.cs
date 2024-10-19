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
    public class UserFavRestaurantRepository : IUserFavRestaurantRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IRestaurantService _restaurantService;

        public UserFavRestaurantRepository(IConfiguration configuration, IUserService userService, IRestaurantService restaurantService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
            _restaurantService = restaurantService;
        }

        public async Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(Guid userId)
        {
            List<UserFavRestaurant> restaurants = new List<UserFavRestaurant>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM User_Fav_Restaurants WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        UserFavRestaurant restaurant = new UserFavRestaurant(
                            Guid.Parse(reader["userId"].ToString()),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            (int)reader["restaurantRank"]
                        );
                        await restaurant.AssignUserAndRestaurant(_userService, _restaurantService);
                        restaurants.Add(restaurant);
                    }
                }
            }

            return restaurants;
        }

        public async Task CreateUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO UserFavRestaurants (userId, restaurantId, restaurantRank) VALUES (@userId, @restaurantId, @restaurantRank)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userFavRestaurant.UserId);
                command.Parameters.AddWithValue("@restaurantId", userFavRestaurant.RestaurantId);
                command.Parameters.AddWithValue("@restaurantRank", userFavRestaurant.RestaurantRank);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE UserFavRestaurants SET restaurantId = @restaurantId WHERE userId = @userId AND restaurantRank = @restaurantRank";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", userFavRestaurant.RestaurantId);
                command.Parameters.AddWithValue("@userId", userFavRestaurant.UserId);
                command.Parameters.AddWithValue("@restaurantRank", userFavRestaurant.RestaurantRank);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteUserTopRestaurant(UserFavRestaurant userFavRestaurant)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM UserFavRestaurants WHERE userId = @userId AND restaurantRank = @restaurantRank";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userFavRestaurant.UserId);
                command.Parameters.AddWithValue("@restaurantRank", userFavRestaurant.RestaurantRank);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
