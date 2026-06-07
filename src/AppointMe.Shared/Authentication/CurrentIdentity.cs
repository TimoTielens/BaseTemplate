namespace AppointMe.Shared.Authentication;

public sealed class CurrentIdentity : ICurrentIdentity
{
    private static readonly AsyncLocal<IIdentity?> Current = new();

    public IIdentity? Identity
    {
        get => Current.Value;
        private set => Current.Value = value;
    }

    public IDisposable Change(IIdentity identity)
    {
        var previous = Identity;
        Identity = identity;
        return new Scope(() => Identity = previous);
    }

    private sealed class Scope(Action restore) : IDisposable
    {
        public void Dispose() => restore();
    }
}
