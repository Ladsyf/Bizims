using Bizims.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bizims.Application.Users.Services;

internal class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieSettings _cookieSettings;

    public CookieService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<CookieSettings> cookieSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _cookieSettings = cookieSettings.Value;
    }

    public void SetAccessToken(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = _cookieSettings.HttpOnly,
            Secure = _cookieSettings.Secure,
            SameSite = _cookieSettings.SameSiteMode,
            Expires = DateTime.UtcNow.AddMinutes(_cookieSettings.JwtExpiryInMinutes)
        };
        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieSettings.AccessTokenKey, token, cookieOptions);
    }

    public void SetRefreshToken(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = _cookieSettings.HttpOnly,
            Secure = _cookieSettings.Secure,
            SameSite = _cookieSettings.SameSiteMode,
            Expires = DateTime.UtcNow.AddHours(_cookieSettings.RefreshTokenExpiryInHours)
        };
        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieSettings.RefreshTokenKey, token, cookieOptions);
    }

    public void RemoveTokens()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieSettings.AccessTokenKey);
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieSettings.RefreshTokenKey);
    }
}