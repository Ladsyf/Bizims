using Bizims.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Bizims.Domain.Models.Businesses;

public class Business : WithId
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public User User { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public byte[]? Logo { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public required BusinessSetting Setting { get; set; }
}