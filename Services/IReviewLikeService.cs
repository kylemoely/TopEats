using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IReviewLikeService
    {
        Task<IEnumerable<ReviewLike>> GetReviewLikes(Guid reviewId);
        Task CreateReviewLike(ReviewLike reviewLike);
        Task DeleteReviewLike(ReviewLike reviewLike);
    }
}
