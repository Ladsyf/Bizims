namespace Bizims.Application.Businesses.Dtos;

public class CreateOrUpdateBusinessApiDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte[]? Logo { get; set; }
    public required BusinessSettingsApiDto Setting { get; set; }
}