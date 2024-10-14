using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class ReviewLikeRepository : IReviewLikeRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        
        public ReviewLikeRepository(IConfiguration configuration, IUserService userService, IReviewService reviewService)
        {
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
            _reviewService = reviewService;
        }

        public async Task<IEnumerable<ReviewLike>> GetReviewLikes(int reviewId)
        {
            List<ReviewLike> reviewLikes = new List<ReviewLike>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM ReviewLikes WHERE reviewId = @reviewId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ReviewLike reviewLike = new ReviewLike
                        (
                            (int)reader["reviewId"],
                            (int)reader["userId"]
                        );
                        await reviewLike.AssignUserAndReview(_userService, _reviewService);
                        reviewLikes.Add(reviewLike);
                    }
                }
            }
            return reviewLikes;
        }

        public async Task CreateReviewLike(ReviewLike reviewLike)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO ReviewLikes (reviewId, userId) VALUES (@reviewId, @userId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewLike.reviewId);
                command.Parameters.AddWithValue("@userId", reviewLike.userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
