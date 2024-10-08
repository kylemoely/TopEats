using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
            _restaurantService = restaurantService;
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            (int)reader["reviewId"],
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            (int)reader["restaurantId"],
                            (int)reader["userId"]
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

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews";
                SqlCommand command = new SqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Review review = new Review
                        (
                            (int)reader["reviewId"],
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            (int)reader["restaurantId"],
                            (int)reader["userId"]
                        );
                        await review.AssignUserAndRestaurant(_userService, _restaurantService);
                        reviews.Add(review);
                    }
                }
            }
            return reviews;
        }

        public async Task CreateReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Reviews (rating, reviewText, restaurantId, userId) VALUES (@rating, @reviewText, @restaurantId, @userId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@reviewText", review.reviewText);
                command.Parameters.AddWithValue("@restaurantId", review.restaurantId);
                command.Parameters.AddWithValue("@userId", review.userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Reviews SET rating = @rating, reviewText = @reviewText WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@reviewText", review.reviewText);
                command.Parameters.AddWithValue("@reviewId", review.reviewId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteReview(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Reviews WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}