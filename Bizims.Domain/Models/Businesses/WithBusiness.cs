using System.ComponentModel.DataAnnotations;

namespace Bizims.Domain.Models.Businesses;

public class WithBusiness : WithId
{
    [Required]
    public Guid BusinessId { get; set; }

    [Required]
    public Business Business { get; set; }
}