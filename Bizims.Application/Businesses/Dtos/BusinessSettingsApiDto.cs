namespace Bizims.Application.Businesses.Dtos;

public class BusinessSettingsApiDto
{
    public Guid Id { get; set; }
    public required string PrimaryHex { get; set; }
}