using Bizims.Application.Exceptions;

namespace Bizims.Application.Users.Services;

public class MultitenantProvider : IMultitenantProvider
{
    public Guid? UserId { get; private set; }
    public Guid? BusinessId { get; private set; }

    public Guid GetUserId()
    {
        return UserId ?? throw new AuthenticationException("Failed to get user id");
    }

    public void SetUserId(Guid userId) => UserId = userId;
    public void SetBusinessId(Guid businessId) => BusinessId = businessId;
}