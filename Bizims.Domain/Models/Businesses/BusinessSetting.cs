using System.ComponentModel.DataAnnotations;

namespace Bizims.Domain.Models.Businesses;

public class BusinessSetting : WithBusiness
{
    [Required]
    [MaxLength(8)]
    public required string PrimaryHex { get; set; }
}