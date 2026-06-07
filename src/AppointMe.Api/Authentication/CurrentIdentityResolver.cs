namespace AppointMe.Api.Authentication;

public sealed class CurrentIdentityResolver(
    ICurrentIdentity currentIdentity,
    HttpIdentityFactory httpIdentityFactory
) : IIdentityResolver
{
    public async ValueTask<IIdentity> Resolve(CancellationToken cancellationToken)
    {
        if (currentIdentity.Identity is { } resolved)
        {
            return resolved;
        }

        var identity = await httpIdentityFactory.Create(cancellationToken);
        currentIdentity.Change(identity);
        return identity;
    }
}
