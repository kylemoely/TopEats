using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
            _restaurantService = restaurantService;
        }

        public async Task<IEnumerable<UserFavRestaurant>> GetUserTopRestaurants(int userId)
        {
            List<UserFavRestaurant> restaurants = new List<UserFavRestaurant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM User_Fav_Restaurants WHERE userId = @userId";
                SqlCommand command = new SqlCommand(query, connection);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        UserFavRestaurant restaurant = new UserFavRestaurant(
                            (int)reader["userId"],
                            (int)reader["restaurantId"],
                            (int)reader["restaurantRank"]
                        );
                        await restaurant.AssignUserAndRestaurant(_userService, _restaurantService);
                        restaurants.Add(restaurant);
                    }
                }
            }

            return restaurants;
        }
    }
}