namespace AppointMe.Shared.Authorization.Principals;

public sealed class CurrentPrincipal : ICurrentPrincipal
{
    private static readonly AsyncLocal<IPrincipal?> Current = new();

    public IPrincipal? Principal
    {
        get => Current.Value;
        private set => Current.Value = value;
    }

    public IDisposable Change(IPrincipal principal)
    {
        var previous = Principal;
        Principal = principal;
        return new Scope(() => Principal = previous);
    }

    private sealed class Scope(Action restore) : IDisposable
    {
        public void Dispose() => restore();
    }
}
