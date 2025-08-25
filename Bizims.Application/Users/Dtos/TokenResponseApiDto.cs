namespace Bizims.Application.Users.Dtos;

public class TokenResponseApiDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}