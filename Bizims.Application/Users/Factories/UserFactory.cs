using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Bizims.Application.Users.Factories;

public class UserFactory : IUserFactory
{
    public User Create(CreateOrUpdateUserRequestApiDto request)
    {
        var hashedPassword = new PasswordHasher<User>()
            .HashPassword(null, request.Password);

        return new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Id = Guid.NewGuid(),
            Username = request.Username,
            MiddleName = request.MiddleName,
            CreatedDate = DateTime.UtcNow,
            PasswordHash = hashedPassword
        };
    }
}