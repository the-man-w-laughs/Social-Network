using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SocialNetwork.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        public const string UserIdContextItem = "UserId";

        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {            
            var isUserAuthenticated = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (isUserAuthenticated.Succeeded)
            {
                var userIdClaim = isUserAuthenticated.Principal?.Claims.
                    FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    context.Items[UserIdContextItem] = (uint)userId;
                }
            }

            await _next(context);
        }
    }

    public static class AuthenticationExtensions
    {
        public static uint GetAuthenticatedUserId(this HttpContext context) => 
            (uint)context.Items[AuthenticationMiddleware.UserIdContextItem]!;
    }
}
