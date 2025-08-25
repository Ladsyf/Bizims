using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;

namespace Bizims.Application.Users.Mappers
{
    public interface IUserApiMapper
    {
        UserApiDto ToDto(User model);
    }
}