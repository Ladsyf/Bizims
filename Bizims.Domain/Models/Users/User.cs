using Bizims.Domain.Models.Businesses;
using System.ComponentModel.DataAnnotations;

namespace Bizims.Domain.Models.Users;

public class User : WithId
{
    [Required]
    [MaxLength(100)]
    public required string Username { get; set; }

    [EmailAddress]
    [Required]
    [MaxLength(200)]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public ICollection<Business> Businesses { get; set; } = [];

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}