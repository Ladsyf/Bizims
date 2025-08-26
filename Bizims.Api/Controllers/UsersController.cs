using Bizims.Application.Users.Dtos;
using Bizims.Application.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bizims.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertUserAsync([FromBody] CreateOrUpdateUserRequestApiDto request)
    { 
        await _userService.InsertAsync(request);

        return SuccessResult.Create(200, "User registered successfully, please log in.");
    }
}