using Bizims.Application.Businesses.Dtos;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Factories;

public class BusinessFactory
{
    public BusinessFactory()
    {
        
    }

    public Business Create(CreateBusinessApiDto request)
    {
        var setting = new BusinessSetting()
        { 
            Id = Guid.NewGuid(),
            PrimaryHex = request.Setting.PrimaryHex
        };

        return new Business()
        { 
            Name = request.Name,
            Setting = setting,
            CreatedDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Description = request.Description,
            Logo = request.Logo,
        };
    }
}