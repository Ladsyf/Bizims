using Bizims.Application.Businesses.Dtos;
using Bizims.Application.Users.Services;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Factories;

internal class BusinessFactory : IBusinessFactory
{
    private readonly IMultitenantProvider _multitenantProvider;

    public BusinessFactory(IMultitenantProvider multitenantProvider)
    {
        _multitenantProvider = multitenantProvider;
    }

    public Business Create(CreateOrUpdateBusinessApiDto request)
    {
        var setting = new BusinessSetting()
        {
            Id = Guid.NewGuid(),
            PrimaryHex = request.Setting.PrimaryHex
        };

        return new Business()
        {
            UserId = _multitenantProvider.GetUserId(),
            Name = request.Name,
            Setting = setting,
            CreatedDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            Description = request.Description,
            Logo = request.Logo,
        };
    }
}