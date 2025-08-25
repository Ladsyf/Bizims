using Bizims.Application.Businesses.Dtos;
using Bizims.Application.Businesses.Mappers;
using Bizims.Domain.Repositories;

namespace Bizims.Application.Businesses.Services;

public class BusinessService
{
    private readonly IBusinessApiMapper _businessApiMapper;
    private readonly IRepositoryManager _repositoryManager;

    public BusinessService(IBusinessApiMapper businessApiMapper, IRepositoryManager repositoryManager)
    {
        _businessApiMapper = businessApiMapper;
        _repositoryManager = repositoryManager;
    }

    public async Task InsertAsync(BusinessApiDto businessApiDto)
    { 
    
    }
}