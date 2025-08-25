using Bizims.Application.Exceptions;
using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;
using Bizims.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Bizims.Application.Users.Services;

internal class AuthValidationService : IAuthValidationService
{
    private readonly IRepositoryManager _repositoryManager;

    public AuthValidationService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<User> ValidateLoginAsync(LoginRequestApiDto request)
    {
        var repo = _repositoryManager.Repository<User>();

        var user = await repo.FindByAsync(x => x.Email == request.Username || x.Username == request.Username);

        if (user == null) ThrowAuthenticationException();

        var wrongPassword = new PasswordHasher<User>().VerifyHashedPassword(user!, user!.PasswordHash, request.Password)
            == PasswordVerificationResult.Failed;

        if (wrongPassword) ThrowAuthenticationException();

        return user!;
    }

    public async Task ValidateRefreshTokenAsync(User user, string refreshToken, Func<Guid, Task> resetRefreshTokenAsync)
    {
        var refreshTokenValid = user.RefreshTokenExpiry != null &&
            user.RefreshToken != null &&
            user.RefreshTokenExpiry >= DateTime.UtcNow &&
            user.RefreshToken == refreshToken;

        if (!refreshTokenValid)
        {
            await resetRefreshTokenAsync(user.Id);
            throw new AuthenticationException("Refresh token expired, please login again");
        }
    }

    private static void ThrowAuthenticationException() => throw new AuthenticationException("Invalid Credentials");
}