using Bizims.Application.Users.Dtos;

namespace Bizims.Application.Users.Services;

public interface IAuthService
{
    Task<TokenResponseApiDto> LoginAsync(LoginRequestApiDto request);
    Task LogoutAsync();
    Task<string> RefreshAccessTokenAsync(Guid id, string refreshToken);
}