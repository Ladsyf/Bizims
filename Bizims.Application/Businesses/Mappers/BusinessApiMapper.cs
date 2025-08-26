using Bizims.Application.Businesses.Dtos;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Mappers;

internal class BusinessApiMapper : IBusinessApiMapper
{
    public BusinessApiDto ToApiDto(Business model)
    {
        var setting = new BusinessSettingsApiDto
        {
            Id = model.Setting.Id,
            PrimaryHex = model.Setting.PrimaryHex
        };

        return new BusinessApiDto
        {
            Name = model.Name,
            Setting = setting,
            CreatedDate = model.CreatedDate,
            Description = model.Description,
            Id = model.Id,
            Logo = model.Logo,
            UserId = model.UserId
        };
    }
}