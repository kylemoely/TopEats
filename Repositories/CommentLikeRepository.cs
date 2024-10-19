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
    public class CommentLikeRepository : ICommentLikeRepository
    {
        private readonly string _connectionString;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        
        public CommentLikeRepository(IConfiguration configuration, IUserService userService, ICommentService commentService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = userService;
            _commentService = commentService;
        }

        public async Task<IEnumerable<CommentLike>> GetCommentLikes(Guid commentId)
        {
            List<CommentLike> commentLikes = new List<CommentLike>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM CommentLikes WHERE commentId = @commentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentId);

                await connection.OpenAsync();
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        CommentLike commentLike = new CommentLike
                        (
                            Guid.Parse(reader["commentId"].ToString()),
                            Guid.Parse(reader["userId"].ToString())
                        );
                        await commentLike.AssignCommentAndUser(_commentService, _userService);
                        commentLikes.Add(commentLike);
                    }
                }
            }
            return commentLikes;
        }

        public async Task CreateCommentLike(CommentLike commentLike)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO CommentLikes (commentId, userId) VALUES (@commentId, @userId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentLike.CommentId);
                command.Parameters.AddWithValue("@userId", commentLike.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteCommentLike(CommentLike commentLike)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM CommentLikes WHERE commentId = @commentId AND userId = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@commentId", commentLike.CommentId);
                command.Parameters.AddWithValue("@userId", commentLike.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
