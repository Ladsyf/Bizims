using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;

namespace Bizims.Application.Users.Factories
{
    public interface IUserFactory
    {
        User Create(CreateOrUpdateUserRequestApiDto request);
    }
}