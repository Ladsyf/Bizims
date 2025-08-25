namespace Bizims.Application.Businesses.Dtos;

public class BusinessApiDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public byte[]? Logo { get; set; }
    public DateTime CreatedDate { get; set; }
    public required BusinessSettingsApiDto Setting { get; set; }
}