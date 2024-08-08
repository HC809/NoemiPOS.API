namespace NoemiPOS.Infraestructure.Multinenacy;
public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid BusinessId { get; }
}
