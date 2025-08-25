using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;

namespace Bizims.Application.Users.Services;

internal interface IAuthValidationService
{
    Task<User> ValidateLoginAsync(LoginRequestApiDto request);
    Task ValidateRefreshTokenAsync(User user, string refreshToken, Func<Guid, Task> resetRefreshTokenAsync);
}