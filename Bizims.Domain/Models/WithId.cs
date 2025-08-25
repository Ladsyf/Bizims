using System.ComponentModel.DataAnnotations;

namespace Bizims.Domain.Models;

public class WithId
{
    [Key]
    public Guid Id { get; set; }
}