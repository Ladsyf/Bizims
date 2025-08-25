namespace Bizims.Application.Users.Dtos;

public class LoginRequestApiDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}