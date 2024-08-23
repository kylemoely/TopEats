using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Review GetReviewById(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Review
                        (
                            (int)reader["reviewId"],
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            (int)reader["restaurantId"],
                            (int)reader["userId"],
                            _userService,
                            _restaurantService
                        );
                    }
                }
            }
            return null;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            List<Review> reviews = new List<Review>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Reviews";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Review
                        (
                            (int)reader["reviewId"],
                            (int)reader["rating"],
                            reader["reviewText"].ToString(),
                            (int)reader["restaurantId"],
                            (int)reader["userId"],
                            _userService,
                            _restaurantService
                        ));
                    }
                }
            }
            return reviews;
        }

        public void CreateReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Reviews (rating, reviewText, restaurantId, userId) VALUES (@rating, @reviewText, @restaurantId, @userId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@reviewText", review.reviewText);
                command.Parameters.AddWithValue("@restaurantId", review.restaurantId);
                command.Parameters.AddWithValue("@userId", review.userId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateReview(Review review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Reviews SET rating = @rating, reviewText = @reviewText WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@reviewText", review.reviewText);
                command.Parameters.AddWithValue("@reviewId", review.reviewId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteReview(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Reviews WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}