using Bizims.Application.Exceptions;
using Bizims.Application.Settings;
using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;
using Bizims.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Bizims.Application.Users.Services;

internal class AuthService : IAuthService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly JwtSettings _jwtSettings;
    private readonly IAuthValidationService _authValidationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<User> _userRepository;

    public AuthService(
        IRepositoryManager repositoryManager,
        ITokenGeneratorService tokenGeneratorService,
        IOptions<JwtSettings> jwtSettings,
        IAuthValidationService authValidationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _tokenGeneratorService = tokenGeneratorService;
        _jwtSettings = jwtSettings.Value;
        _authValidationService = authValidationService;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = _repositoryManager.Repository<User>();
    }

    public async Task<TokenResponseApiDto> LoginAsync(LoginRequestApiDto request)
    {
        var validatedUser = await _authValidationService.ValidateLoginAsync(request);

        var generatedTokens = GenerateTokens(validatedUser);

        await _userRepository
            .UpdateAsync(x => x.Id == validatedUser.Id, (user) =>
            {
                user.RefreshToken = generatedTokens.RefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(_jwtSettings.RefreshTokenExpiryInHours);
            });

        await _repositoryManager.SaveAsync();

        return generatedTokens;
    }

    public async Task LogoutAsync()
    {
        var userClaims = _httpContextAccessor.HttpContext.User;
        var stringId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (stringId == null || !Guid.TryParse(stringId, out Guid userId))
            throw new AuthenticationException("You are not authenticated to perform this action");

        await ResetUserRefreshTokenAsync(userId);
    }

    public async Task<string> RefreshAccessTokenAsync(Guid id, string refreshToken)
    {
        var user = await _userRepository.FindByAsync(x => x.Id == id);

        if (user == null)
            throw new AuthenticationException("User not found, please log in again.");

        await _authValidationService.ValidateRefreshTokenAsync(user, refreshToken, ResetUserRefreshTokenAsync);

        return _tokenGeneratorService.GenerateJwtToken(user);
    }

    private async Task ResetUserRefreshTokenAsync(Guid userId)
    {
        await _userRepository
        .UpdateAsync(x => x.Id == userId, (user) =>
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
        });

        await _repositoryManager.SaveAsync();
    }

    private TokenResponseApiDto GenerateTokens(User user)
    {
        var jwtToken = _tokenGeneratorService.GenerateJwtToken(user);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        return new TokenResponseApiDto { AccessToken = jwtToken, RefreshToken = refreshToken };
    }
}