namespace AppointMe.Shared.Companies;

public sealed class CurrentCompany : ICurrentCompany
{
    private static readonly AsyncLocal<CompanyId?> Current = new();

    public CompanyId? CompanyId
    {
        get => Current.Value;
        private set => Current.Value = value;
    }

    public IDisposable Change(CompanyId companyId)
    {
        var previous = CompanyId;
        CompanyId = companyId;
        return new Scope(() => CompanyId = previous);
    }

    private sealed class Scope(Action restore) : IDisposable
    {
        public void Dispose() => restore();
    }
}
