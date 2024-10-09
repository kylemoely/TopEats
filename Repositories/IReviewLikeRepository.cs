using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Repositories
{
    public interface IReviewLikeRepository
    {
        Task<IEnumerable<ReviewLike>> GetReviewLikes(int reviewId);
    }
}