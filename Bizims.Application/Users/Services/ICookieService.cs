namespace Bizims.Application.Users.Services;

public interface ICookieService
{
    void RemoveTokens();
    void SetAccessToken(string token);
    void SetRefreshToken(string token);
}