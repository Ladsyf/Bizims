using Bizims.Application.Exceptions;
using Bizims.Application.Users.Dtos;
using Bizims.Domain.Models.Users;
using Bizims.Domain.Repositories;

namespace Bizims.Application.Users.Services;

internal class UserValidationService : IUserValidationService
{
    private readonly IRepository<User> _repository;

    public UserValidationService(IRepositoryManager repositoryManager)
    {
        _repository = repositoryManager.Repository<User>();
    }

    public async Task ValidateCreateAsync(CreateOrUpdateRequestApiDto request)
    {
        var existingUser = await _repository.FindByAsync(x => x.Username == request.Username || x.Email == request.Email);

        if (existingUser != null) throw new ValidationException("User already exists");
    }
}