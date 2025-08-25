using Bizims.Application.Users.Dtos;

namespace Bizims.Application.Users.Services;

public interface IUserService
{
    Task DeleteAsync(Guid id);
    Task<UserApiDto> GetAsync(Guid id);
    Task InsertAsync(CreateOrUpdateRequestApiDto request);
    Task UpdateAsync(Guid id, CreateOrUpdateRequestApiDto request);
}