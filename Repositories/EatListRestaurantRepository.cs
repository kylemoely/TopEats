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

        public UserFavRestaurantRepository(IConfiguration configuration, IEatListService eatListService, IRestaurantService restaurantService)
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
    }
}
