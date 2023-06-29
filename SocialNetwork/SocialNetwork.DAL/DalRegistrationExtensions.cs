using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.DAL.Context;
using SocialNetwork.DAL.Contracts.Chats;
using SocialNetwork.DAL.Contracts.Comments;
using SocialNetwork.DAL.Contracts.Communities;
using SocialNetwork.DAL.Contracts.Medias;
using SocialNetwork.DAL.Contracts.Messages;
using SocialNetwork.DAL.Contracts.Posts;
using SocialNetwork.DAL.Contracts.Users;
using SocialNetwork.DAL.Repositories.Chats;
using SocialNetwork.DAL.Repositories.Comments;
using SocialNetwork.DAL.Repositories.Communities;
using SocialNetwork.DAL.Repositories.Medias;
using SocialNetwork.DAL.Repositories.Messages;
using SocialNetwork.DAL.Repositories.Posts;
using SocialNetwork.DAL.Repositories.Users;

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
            builder.UseLazyLoadingProxies().UseMySQL(connectionString);
        });

        // Chats
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IChatMemberRepository, ChatMemberRepository>();

        // Comments
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentLikeRepository, CommentLikeRepository>();        
        
        // Communities
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<ICommunityMemberRepository, CommunityMemberRepository>();        
        
        // Medias
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMediaLikeRepository, MediaLikeRepository>();        

        // Messages
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IMessageLikeRepository, MessageLikeRepository>();
        services.AddScoped<IMessageMediaRepository, MessageMediaRepository>();
        
        // Posts
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostLikeRepository, PostLikeRepository>();        
        
        // Users
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserFollowerRepository, UserFollowerRepository>();
        services.AddScoped<IUserFriendRepository, UserFriendRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserProfileMediaRepository, UserProfileMediaRepository>();        
    }
}