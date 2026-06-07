namespace AppointMe.Shared.Authentication;

public interface IIdentityResolver
{
    ValueTask<IIdentity> Resolve(CancellationToken cancellationToken);
}
