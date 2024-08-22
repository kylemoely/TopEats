using System;
using Microsoft.Extensions.DependencyInjection;
using TopEats.Repositories

namespace TopEats.Services
{

    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
        }
    }

}
