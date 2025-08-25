using Bizims.Application.Businesses.Dtos;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Mappers
{
    public interface IBusinessApiMapper
    {
        BusinessApiDto ToApiDto(Business model);
        Business ToModel(BusinessApiDto model);
    }
}