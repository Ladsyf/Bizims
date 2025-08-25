using Microsoft.AspNetCore.Http;

namespace Bizims.Application.Settings;

public class CookieSettings
{
    public bool HttpOnly { get; set; }
    public CookieSecurePolicy CookieSecurePolicy { get; set; }
    public SameSiteMode SameSiteMode { get; set; }
    public int JwtExpiryInMinutes { get; set; }
    public int RefreshTokenExpiryInHours { get; set; }
    public bool Secure { get; set; }

    public const string AccessTokenKey = "access_token";
    public const string RefreshTokenKey = "refresh_token";
}