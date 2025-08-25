using Bizims.Application.Settings;
using Bizims.Domain.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bizims.Application.Users.Services;

internal class TokenGeneratorService : ITokenGeneratorService
{
    private readonly JwtSettings _jwtConfiguration;

    public TokenGeneratorService(IOptions<JwtSettings> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiryInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}