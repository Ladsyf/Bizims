using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;

namespace Bizims.Application.Users.Mappers;

public class UserApiMapper : IUserApiMapper
{
    public UserApiDto ToDto(User model)
    {
        return new UserApiDto
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            CreatedDate = model.CreatedDate,
            Id = model.Id,
            MiddleName = model.MiddleName
        };
    }
}