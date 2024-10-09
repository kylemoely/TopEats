using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TopEats.Models;

namespace TopEats.Services
{
    public interface IReviewLikeService
    {
        Task<IEnumerable<ReviewLike>> GetReviewLikes(int reviewId);
    }
}