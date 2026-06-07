namespace AppointMe.Api.Wolverine.HandlerContext;

public static class IdentityContextBehavior
{
    public static async Task<IIdentity> Load(IIdentityResolver identityResolver,
        CancellationToken cancellationToken)
    {
        return await identityResolver.Resolve(cancellationToken);
    }
}
