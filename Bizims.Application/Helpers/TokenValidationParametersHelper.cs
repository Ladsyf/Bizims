using Bizims.Application.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bizims.Application.Helpers;

public static class TokenValidationParametersHelper
{
    public static TokenValidationParameters Create(JwtSettings jwtSettings)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}