using Bizims.Application.Businesses.Dtos;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Mappers;

internal interface IBusinessApiMapper
{
    BusinessApiDto ToApiDto(Business model);
}