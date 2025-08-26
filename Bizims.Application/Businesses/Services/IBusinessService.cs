using Bizims.Application.Businesses.Dtos;

namespace Bizims.Application.Businesses.Services
{
    public interface IBusinessService
    {
        Task DeleteAsync(Guid id);
        Task<IReadOnlyList<BusinessApiDto>> FindAllAsync();
        Task<BusinessApiDto> GetAsync(Guid id);
        Task InsertAsync(CreateOrUpdateBusinessApiDto request);
        Task UpdateAsync(Guid id, CreateOrUpdateBusinessApiDto request);
    }
}