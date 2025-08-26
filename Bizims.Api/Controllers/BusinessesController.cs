using Bizims.Application.Businesses.Dtos;
using Bizims.Application.Businesses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bizims.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessesController : ControllerBase
{
    private readonly IBusinessService _businessService;

    public BusinessesController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponseApiDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBusinessAsync([FromBody] CreateOrUpdateBusinessApiDto request)
    {
        await _businessService.InsertAsync(request);

        return SuccessResult.Create(201, "Business created successfully.");
    }

    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponseApiDto<IReadOnlyList<BusinessApiDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> FindAllBusinessesAsync()
    {
        var businesses = await _businessService.FindAllAsync();

        return SuccessResult.Create(200, businesses);
    }

    [HttpGet("{businessId}")]
    [ProducesResponseType(typeof(SuccessResponseApiDto<BusinessApiDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBusinessAsync(Guid businessId)
    {
        var business = await _businessService.GetAsync(businessId);

        return SuccessResult.Create(200, business);
    }

    [HttpPatch("{businessId}")]
    [ProducesResponseType(typeof(SuccessResponseApiDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateBusinessAsync(Guid businessId, [FromBody] CreateOrUpdateBusinessApiDto request)
    { 
        await _businessService.UpdateAsync(businessId, request);

        return SuccessResult.Create(201, "Business updated successfully.");
    }

    [HttpDelete("{businessId}")]
    [ProducesResponseType(typeof(SuccessResponseApiDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBusinessAsync(Guid businessId)
    { 
        await _businessService.DeleteAsync(businessId);

        return SuccessResult.Create(200, "Business deleted successfully.");
    }
}