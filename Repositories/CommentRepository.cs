using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public CommentRepository(IConfiguration configuration, IUserService userService, IReviewService reviewService)
        {
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
            _reviewService = reviewService;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Comment comment = new Comment(
                            (int)reader["commentId"],
                            (int)reader["reviewId"],
                            (int)reader["userId"],
                            reader["commentText"].ToString()
                        );
                        comment.AssignUserAndReview(_userService, _reviewService);
                        return comment;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Comment comment = new Comment
                        (
                            (int)reader["commentId"], 
                            (int)reader["reviewId"], 
                            (int)reader["userId"], 
                            reader["commentText"].ToString()
                        );
                        comment.AssignUserAndReview(_userService, _reviewService);
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }

        public async Task CreateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Comments (reviewId, userId, commentText) VALUES (@reviewId, @userId, @commentText)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", comment.reviewId);
                command.Parameters.AddWithValue("@userId", comment.userId);
                command.Parameters.AddWithValue("@commentText", comment.commentText);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Comments SET commentText = @commentText WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentText", comment.commentText);
                command.Parameters.AddWithValue("@commentId", comment.commentId);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteComment(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Comments WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                connection.OpenAsync();
                command.ExecuteNonQueryAsync();
            }
        }
    }
}