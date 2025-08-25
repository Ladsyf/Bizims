using Bizims.Application.Exceptions;
using Bizims.Application.Settings;
using Bizims.Application.Users.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bizims.Api.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JwtMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var token = httpContext.Request.Cookies[CookieSettings.AccessTokenKey];
        using var scope = _serviceScopeFactory.CreateAsyncScope();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                SetUserInContext(scope, handler, token, httpContext);
            }
            catch (Exception ex)
            {
                if (ex is not SecurityTokenExpiredException) throw;

                var cookieService = scope.ServiceProvider.GetRequiredService<ICookieService>();

                try
                {
                    var handler = new JwtSecurityTokenHandler();

                    var principal = handler.ReadJwtToken(token);

                    var stringId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
                    var refreshToken = httpContext.Request.Cookies[CookieSettings.RefreshTokenKey]!;

                    if (!Guid.TryParse(stringId, out var userId))
                        throw new AuthenticationException("Refresh token expired, please log in again.");

                    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                    var newToken = await authService.RefreshAccessTokenAsync(userId, refreshToken);
                    cookieService.SetAccessToken(newToken);

                    SetUserInContext(scope, handler, newToken, httpContext);
                }
                catch (Exception innerEx)
                {
                    if (innerEx is AuthenticationException)
                    {
                        cookieService.RemoveTokens();
                    }

                    throw;
                }
            }
        }

        await _next(httpContext);
    }

    private static void SetUserInContext(AsyncServiceScope scope, JwtSecurityTokenHandler handler, string token, HttpContext httpContext)
    {
        var validationParams = scope.ServiceProvider.GetRequiredService<TokenValidationParameters>();

        var principal = handler.ValidateToken(token, validationParams, out var _);

        httpContext.User = principal;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}
