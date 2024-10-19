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
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public CommentRepository(IConfiguration configuration, IUserService userService, IReviewService reviewService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
            _reviewService = reviewService;
        }

        public async Task<Comment> GetCommentById(Guid commentId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments WHERE commentId = @commentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Comment comment = new Comment(
                            Guid.Parse(reader["commentId"].ToString()), 
                            Guid.Parse(reader["reviewId"].ToString()), 
                            Guid.Parse(reader["userId"].ToString()), 
                            reader["commentText"].ToString()
                        );
                        await comment.AssignUserAndReview(_userService, _reviewService);
                        return comment;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            List<Comment> comments = new List<Comment>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments";
                MySqlCommand command = new MySqlCommand(query, connection);
                
                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Comment comment = new Comment
                        (
                            Guid.Parse(reader["commentId"].ToString()), 
                            Guid.Parse(reader["reviewId"].ToString()), 
                            Guid.Parse(reader["userId"].ToString()), 
                            reader["commentText"].ToString()
                        );
                        await comment.AssignUserAndReview(_userService, _reviewService);
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }

        public async Task CreateComment(Comment comment)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO Comments (reviewId, userId, commentText) VALUES (@reviewId, @userId, @commentText)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", comment.ReviewId);
                command.Parameters.AddWithValue("@userId", comment.UserId);
                command.Parameters.AddWithValue("@commentText", comment.CommentText);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE Comments SET commentText = @commentText WHERE commentId = @commentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentText", comment.CommentText);
                command.Parameters.AddWithValue("@commentId", comment.CommentId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteComment(Guid commentId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString)){
                string query = "DELETE FROM Comments WHERE commentId = @commentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetReviewComments(Guid reviewId)
        {
            List<Comment> comments = new List<Comment>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments WHERE reviewId = @reviewId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", reviewId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Comment comment = new Comment
                        (
                            Guid.Parse(reader["commentId"].ToString()), 
                            Guid.Parse(reader["reviewId"].ToString()), 
                            Guid.Parse(reader["userId"].ToString()), 
                            reader["commentText"].ToString()
                        );
                        await comment.AssignUserAndReview(_userService, _reviewService);
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }
    }
}