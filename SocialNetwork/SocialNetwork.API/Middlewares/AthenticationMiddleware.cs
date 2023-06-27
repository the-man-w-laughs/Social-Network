using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
namespace SocialNetwork.API.Middlewares
{
    public class AuthenticationMiddleware
    {
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
                    context.Items["UserId"] = (uint)userId;
                }
            }

            await _next(context);
        }
    }

}
