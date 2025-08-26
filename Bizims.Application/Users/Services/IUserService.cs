using Bizims.Application.Users.Dtos;

namespace Bizims.Application.Users.Services;

public interface IUserService
{
    Task DeleteAsync(Guid id);
    Task<UserApiDto> GetAsync(Guid id);
    Task InsertAsync(CreateOrUpdateUserRequestApiDto request);
    Task UpdateAsync(Guid id, CreateOrUpdateUserRequestApiDto request);
}