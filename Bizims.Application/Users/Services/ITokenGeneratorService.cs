using Bizims.Domain.Models.Users;

namespace Bizims.Application.Users.Services;

internal interface ITokenGeneratorService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}