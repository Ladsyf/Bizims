using Bizims.Application.Users.Dtos;
using Bizims.Application.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bizims.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICookieService _cookieService;

    public AuthController(IAuthService authService, ICookieService cookieService)
    {
        _authService = authService;
        _cookieService = cookieService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestApiDto request)
    {
        var token = await _authService.LoginAsync(request);

        _cookieService.SetAccessToken(token.AccessToken);

        _cookieService.SetRefreshToken(token.RefreshToken);

        return SuccessResult.Create(200, "Logged in successfully.");
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(SuccessResponseApiDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> LogoutAsync()
    {
        _cookieService.RemoveTokens();

        await _authService.LogoutAsync();

        return SuccessResult.Create(200, "Logged out successfully.");
    }
}