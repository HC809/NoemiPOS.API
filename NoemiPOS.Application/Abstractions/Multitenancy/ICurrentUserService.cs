namespace NoemiPOS.Application.Abstractions.Multitenancy;
public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid BusinessId { get; }
    bool IsBusinessUser { get; }
}
