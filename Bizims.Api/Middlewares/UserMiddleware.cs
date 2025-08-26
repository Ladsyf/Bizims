using Bizims.Application.Users.Services;
using System.Security.Claims;

namespace Bizims.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IMultitenantProvider multitenantProvider)
        {
            var principal = httpContext.User;

            var stringId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (stringId != null)
            {
                if (!Guid.TryParse(stringId, out Guid userId)) throw new Exception("Failed to get userid");

                (multitenantProvider as MultitenantProvider)!.SetUserId(userId);
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserMiddleware>();
        }
    }
}
