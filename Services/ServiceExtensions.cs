using System;
using Microsoft.Extensions.DependencyInjection;
using TopEats.Repositories;

namespace TopEats.Services
{

    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IEatListRepository, EatListRepository>();
            services.AddScoped<IEatListService, EatListService>();
            services.AddScoped<IUserFavRestaurantRepository, UserFavRestaurantRepository>();
            services.AddScoped<IUserFavRestaurantService, UserFavRestaurantService>();
            services.AddScoped<IReviewLikeRepository, ReviewLikeRepository>();
            services.AddScoped<IReviewLikeService, ReviewLikeService>();
            services.AddScoped<ICommentLikeRepository, CommentLikeRepository>();
            services.AddScoped<ICommentLikeService, CommentLikeService>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IEatListRestaurantRepository, EatListRestaurantRepository>();
            services.AddScoped<IEatListRestaurantService, EatListRestaurantService>();
        }
    }

}
