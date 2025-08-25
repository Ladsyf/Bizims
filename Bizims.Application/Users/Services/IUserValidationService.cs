using Bizims.Application.Users.Dtos;

namespace Bizims.Application.Users.Services;

internal interface IUserValidationService
{
    Task ValidateCreateAsync(CreateOrUpdateRequestApiDto request);
}