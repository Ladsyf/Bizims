using Bizims.Application.Businesses.Dtos;
using Bizims.Application.Businesses.Factories;
using Bizims.Application.Businesses.Mappers;
using Bizims.Application.Exceptions;
using Bizims.Application.Users.Services;
using Bizims.Domain.Models.Businesses;
using Bizims.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bizims.Application.Businesses.Services;

internal class BusinessService : IBusinessService
{
    private readonly IBusinessApiMapper _businessApiMapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IBusinessFactory _businessFactory;
    private readonly IRepository<Business> _repository;
    private readonly IMultitenantProvider _multitenantProvider;

    public BusinessService(
        IBusinessApiMapper businessApiMapper,
        IRepositoryManager repositoryManager,
        IBusinessFactory businessFactory,
        IMultitenantProvider multitenantProvider)
    {
        _businessApiMapper = businessApiMapper;
        _repositoryManager = repositoryManager;
        _businessFactory = businessFactory;
        _repository = repositoryManager.Repository<Business>();
        _multitenantProvider = multitenantProvider;
    }

    public async Task InsertAsync(CreateOrUpdateBusinessApiDto request)
    {
        var newBusiness = _businessFactory.Create(request);

        await _repository.InsertAsync(newBusiness);

        await _repositoryManager.SaveAsync();
    }

    public async Task<IReadOnlyList<BusinessApiDto>> FindAllAsync()
    {
        var userId = _multitenantProvider.GetUserId();

        var businesses = await _repository.FindAllByAsync(x => x.UserId == userId, b => b.Include(x => x.Setting));

        return businesses.Select(_businessApiMapper.ToApiDto).ToList();
    }

    public async Task<BusinessApiDto> GetAsync(Guid id)
    { 
        var business = await _repository.FindByAsync(x => x.Id == id, b => b.Include(x => x.Setting));

        if (business == null)
            throw new NotFoundException("Business not found.");

        return _businessApiMapper.ToApiDto(business);
    }

    public async Task UpdateAsync(Guid id, CreateOrUpdateBusinessApiDto request)
    {
        await _repository.UpdateAsync(x => x.Id == id, (business) =>
        {
            business.Name = request.Name;
            business.Description = request.Description;
            business.Setting.PrimaryHex = request.Setting.PrimaryHex;
        });

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    { 
        await _repository.DeleteAsync(x => x.Id == id);

        await _repositoryManager.SaveAsync();
    }
}