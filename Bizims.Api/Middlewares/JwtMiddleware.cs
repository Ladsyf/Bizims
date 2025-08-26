using Bizims.Application.Exceptions;
using Bizims.Application.Helpers;
using Bizims.Application.Settings;
using Bizims.Application.Users.Services;
using Microsoft.Extensions.Options;
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

    public async Task Invoke(
        HttpContext httpContext,
        ICookieService cookieService,
        IAuthService authService,
        IOptions<JwtSettings> jwtSettings)
    {
        var token = httpContext.Request.Cookies[CookieSettings.AccessTokenKey];
        var refreshToken = httpContext.Request.Cookies[CookieSettings.RefreshTokenKey]!;

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                SetUserInContext(handler, token, httpContext, jwtSettings.Value);
            }
            catch (Exception ex)
            {
                if (ex is not SecurityTokenExpiredException) throw;

                var handler = new JwtSecurityTokenHandler();

                var principal = handler.ReadJwtToken(token);

                var stringId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

                if (!Guid.TryParse(stringId, out var userId))
                    throw new AuthenticationException("Refresh token expired, please log in again.");

                var newToken = await authService.RefreshAccessTokenAsync(userId, refreshToken);
                cookieService.SetAccessToken(newToken);

                SetUserInContext(handler, newToken, httpContext, jwtSettings.Value);
            }
        }
        else if (!string.IsNullOrEmpty(token))
        { 
            cookieService.RemoveTokens();
        }

        await _next(httpContext);
    }

    private static void SetUserInContext(JwtSecurityTokenHandler handler, string token, HttpContext httpContext, JwtSettings settings)
    {
        var validationParams = TokenValidationParametersHelper.Create(settings);

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
