using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IReviewLikeRepository
    {
        Task<IEnumerable<ReviewLike>> GetReviewLikes(int reviewId);
        Task CreateReviewLike(ReviewLike reviewLike);
        Task DeleteReviewLike(ReviewLike reviewLike);
    }
}
