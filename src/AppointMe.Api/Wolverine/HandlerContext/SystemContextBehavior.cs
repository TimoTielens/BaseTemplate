namespace AppointMe.Api.Wolverine.HandlerContext;

public static class SystemContextBehavior
{
    public static Task Apply(ICurrentIdentity currentIdentity, ICurrentPrincipal currentPrincipal)
    {
        currentIdentity.Change(new SystemIdentity());
        currentPrincipal.Change(new SystemPrincipal());

        return Task.CompletedTask;
    }
}
