
namespace Bizims.Application.Users.Services;

public interface IMultitenantProvider
{
    Guid? BusinessId { get; }
    Guid? UserId { get; }
}