using Bizims.Application.Businesses.Dtos;
using Bizims.Domain.Models.Businesses;

namespace Bizims.Application.Businesses.Factories;

internal interface IBusinessFactory
{
    Business Create(CreateOrUpdateBusinessApiDto request);
}