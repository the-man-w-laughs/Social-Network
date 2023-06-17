using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.DAL.Context;

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

        // TODO - register repositories
        // services.AddScoped<UserRepository>();
        // or
        // services.AddScoped<IUserRepository, UserRepository>();
    }
}