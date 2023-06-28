using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.BLL.AutoMapper;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Services.Auth;
using SocialNetwork.BLL.Services.File;

namespace SocialNetwork.BLL;

public static class BllRegistrationExtensions
{
    public static void RegisterBllDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ChatsProfile));
        services.AddAutoMapper(typeof(CommentsProfile));
        services.AddAutoMapper(typeof(CommunitiesProfile));
        services.AddAutoMapper(typeof(CommunitiesProfile));
        services.AddAutoMapper(typeof(MediasProfile));
        services.AddAutoMapper(typeof(MessagesProfile));
        services.AddAutoMapper(typeof(PostsProfile));
        services.AddAutoMapper(typeof(UsersProfile));
        
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IChatService,ChatService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICommunityService,CommunityService>();
        services.AddScoped<IMessageService, MessageService>();
    }
}