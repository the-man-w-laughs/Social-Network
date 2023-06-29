using Newtonsoft.Json;

namespace SocialNetwork.API.Extensions
{
    public static class ControllersExtensions
    {
        public static IMvcBuilder AddControllersWithNewtonsoftJson(this IServiceCollection services)
        {
            return services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }
    }
}
