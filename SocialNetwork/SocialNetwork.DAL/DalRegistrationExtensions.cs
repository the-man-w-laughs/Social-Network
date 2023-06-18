using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.DAL;

public static class DalRegistrationExtensions
{
    public static void RegisterDalDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SocialNetworkContext>(builder =>
        {
            var connectionString = configuration.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string cannot be empty.");

            builder.UseMySQL(connectionString);
        });

        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}