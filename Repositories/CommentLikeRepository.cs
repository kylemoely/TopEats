using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TopEats.Models;
using TopEats.Services;

namespace TopEats.Repositories
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        
        public CommentLikeRepository(IConfiguration configuration, IUserService userService, ICommentService commentService)
        {
            _connectionString = Environment.GetEnvironmentVariable("connection_string");
            _userService = userService;
            _commentService = commentService;
        }

        public async Task<IEnumerable<CommentLike>> GetCommentLikes(int commentId)
        {
            List<CommentLike> commentLikes = new List<CommentLike>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CommentLikes WHERE commentId = @commentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        CommentLike commentLike = new CommentLike
                        (
                            (int)reader["commentId"],
                            (int)reader["userId"]
                        );
                        await commentLike.AssignCommentAndUser(_commentService, _userService);
                        commentLikes.Add(commentLike);
                    }
                }
            }
            return commentLikes;
        }

        public async Task CreateCommentLike(int commentId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO CommentLikes (commentId, userId) VALUES (@commentId, @userId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);
                command.Parameters.AddWithValue("@userId", userId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
