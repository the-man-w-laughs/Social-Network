using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.BLL;

public static class BllRegistrationExtensions
{
    public static void RegisterBllDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO - register services
        // services.AddAutoMapper(typeof(AutoMapperProfile));
        // services.AddScoped<UserService>();
    }
}