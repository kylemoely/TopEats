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
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IRestaurantService _restaurantService;

        public ReviewRepository(IConfiguration configuration, IUserService userService, IRestaurantService restaurantService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
            _restaurantService = restaurantService;
        }

        public async Task<Review> GetReviewById(Guid reviewId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews WHERE reviewId = @reviewId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            Guid.Parse(reader["reviewId"].ToString()),
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        return review;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews";
                MySqlCommand command = new MySqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            Guid.Parse(reader["reviewId"].ToString()),
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }

        public async Task<Review> CreateReview(Review review)
        {
            Guid reviewId = Guid.NewGuid();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Reviews (reviewId, rating, reviewText, restaurantId, userId) VALUES (@reviewId, @rating, @reviewText, @restaurantId, @userId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);
                command.Parameters.AddWithValue("@rating", review.Rating);
                command.Parameters.AddWithValue("@reviewText", review.ReviewText);
                command.Parameters.AddWithValue("@restaurantId", review.RestaurantId);
                command.Parameters.AddWithValue("@userId", review.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                review.ReviewId = reviewId;

                return review;
            }
        }

        public async Task UpdateReview(Review review)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Reviews SET rating = @rating, reviewText = @reviewText WHERE reviewId = @reviewId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@rating", review.Rating);
                command.Parameters.AddWithValue("@reviewText", review.ReviewText);
                command.Parameters.AddWithValue("@reviewId", review.ReviewId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteReview(Guid reviewId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString)){
                string query = "DELETE FROM Reviews WHERE reviewId = @reviewId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetUserReviews(Guid userId)
        {
            List<Review> reviews = new List<Review>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews WHERE userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            Guid.Parse(reader["reviewId"].ToString()),
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }

        public async Task<IEnumerable<Review>> GetRestaurantReviews(Guid restaurantId)
        {
            List<Review> reviews = new List<Review>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews WHERE restaurantId = @restaurantId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@restaurantId", restaurantId);

                await connection.OpenAsync();
                using(MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            Guid.Parse(reader["reviewId"].ToString()),
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }

        public async Task<IEnumerable<Review>> GetFolloweeReviews(Guid userId)
        {
            List<Review> reviews = new List<Review>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT r.* FROM Reviews r INNER JOIN Follows f ON r.userId = f.followeeId WHERE f.followerId = @userId ORDER BY createdAt DESC";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            Guid.Parse(reader["reviewId"].ToString()),
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            Guid.Parse(reader["restaurantId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }
    }
}
