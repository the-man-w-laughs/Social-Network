using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Services.Auth;
using SocialNetwork.BLL.Services.File;

namespace SocialNetwork.BLL;

public static class BllRegistrationExtensions
{
    public static void RegisterBllDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        
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