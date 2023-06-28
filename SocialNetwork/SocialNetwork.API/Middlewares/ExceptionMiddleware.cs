using System.Net;
using SocialNetwork.BLL.Exceptions;

namespace SocialNetwork.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OwnershipException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (AccessDeniedException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (DuplicateEntryException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (LoggedInUserAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (WrongCredentialsException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

}
