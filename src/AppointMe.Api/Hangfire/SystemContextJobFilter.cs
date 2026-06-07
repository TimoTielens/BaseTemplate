using Hangfire.Server;

namespace AppointMe.Api.Hangfire;

public sealed class SystemContextJobFilter(ICurrentIdentity currentIdentity, ICurrentPrincipal currentPrincipal)
    : IServerFilter
{
    private const string StateKey = "SystemContextJobFilter.State";

    public void OnPerforming(PerformingContext context)
    {
        var identityScope = currentIdentity.Change(new SystemIdentity());
        var principalScope = currentPrincipal.Change(new SystemPrincipal());

        context.Items[StateKey] = new State(identityScope, principalScope);
    }

    public void OnPerformed(PerformedContext context)
    {
        if (!context.Items.TryGetValue(StateKey, out var value) || value is not State state)
        {
            return;
        }

        state.PrincipalScope.Dispose();
        state.IdentityScope.Dispose();
    }

    private sealed record State(IDisposable IdentityScope, IDisposable PrincipalScope);
}
