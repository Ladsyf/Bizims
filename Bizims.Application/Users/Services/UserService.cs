using Bizims.Application.Exceptions;
using Bizims.Application.Users.Dtos;
using Bizims.Application.Users.Factories;
using Bizims.Application.Users.Mappers;
using Bizims.Domain.Models.Users;
using Bizims.Domain.Repositories;

namespace Bizims.Application.Users.Services;

internal class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IRepository<User> _repository;
    private readonly IUserValidationService _userValidationService;
    private readonly IUserFactory _userFactory;
    private readonly IUserApiMapper _userApiMapper;

    public UserService(
        IRepositoryManager repositoryManager,
        IUserValidationService userValidationService,
        IUserFactory userFactory,
        IUserApiMapper userApiMapper)
    {
        _repositoryManager = repositoryManager;
        _repository = repositoryManager.Repository<User>();
        _userValidationService = userValidationService;
        _userFactory = userFactory;
        _userApiMapper = userApiMapper;
    }

    public async Task InsertAsync(CreateOrUpdateUserRequestApiDto request)
    {
        await _userValidationService.ValidateCreateAsync(request);

        var newUser = _userFactory.Create(request);

        await _repository.InsertAsync(newUser);

        await _repositoryManager.SaveAsync();
    }

    public async Task<UserApiDto> GetAsync(Guid id)
    {
        var user = await _repository.FindByAsync(x => x.Id == id);

        if (user == null) throw new NotFoundException("User not found");

        return _userApiMapper.ToDto(user);
    }

    public async Task UpdateAsync(Guid id, CreateOrUpdateUserRequestApiDto request)
    {
        await _repository.UpdateAsync(x => x.Id == id, user =>
        {
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.MiddleName = request.MiddleName;
        });

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(x => x.Id == id);
        await _repositoryManager.SaveAsync();
    }
}