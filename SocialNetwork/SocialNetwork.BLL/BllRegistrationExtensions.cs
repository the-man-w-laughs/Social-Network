using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.BLL.AutoMapperProfiles;
using SocialNetwork.BLL.Contracts;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Services.Auth;
using SocialNetwork.BLL.Services.File;
using System.Reflection;

namespace SocialNetwork.BLL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterBllDependencies(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(ChatsProfile), typeof(CommentsProfile), typeof(CommunitiesProfile),
                typeof(MediasProfile), typeof(MessagesProfile), typeof(PostsProfile), typeof(UsersProfile));

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
