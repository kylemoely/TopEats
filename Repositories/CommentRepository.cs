using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public CommentRepository(IConfiguration configuration, IUserService userservice, IReviewService reviewService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
            _reviewService = reviewService;
        }

        public Comment GetCommentById(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Comment
                        {
                            _commentId = (int)reader["commentId"],
                            _reviewId = (int)reader["reviewId"],
                            _userId = (int)reader["userId"],
                            _commentText = reader["commentText"].ToString(),
                            userService = _userService,
                            reviewService = _reviewService
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            List<Comment> comments = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Comments";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            _commentId = (int)reader["commentId"],
                            _reviewId = (int)reader["reviewId"],
                            _userId = (int)reader["userId"],
                            _commentText = reader["commentText"].ToString(),
                            userService = _userService,
                            reviewService = _reviewService
                        });
                    }
                }
            }
            return comments;
        }

        public void CreateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Comments (reviewId, userId, commentText) VALUES (@reviewId, @userId, @commentText)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@reviewId", comment.reviewId);
                command.Parameters.AddWithValue("@userId", comment.userId);
                command.Parameters.AddWithValue("@commentText", comment.commentText);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Comments SET commentText = @commentText WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentText", comment.commentText);
                command.Parameters.AddWithValue("@commentId", comment.commentId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteComment(int commentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString)){
                string query = "DELETE FROM Comments WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}