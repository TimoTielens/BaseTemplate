using Wolverine;

namespace AppointMe.Api.Wolverine.HandlerContext;

public static class TenantContextBehavior
{
    private const string DefaultTenantSentinel = "*DEFAULT*";

    public static Task Apply(ICurrentCompany currentCompany, Envelope envelope)
    {
        if (string.IsNullOrEmpty(envelope.TenantId) || envelope.TenantId == DefaultTenantSentinel)
        {
            return Task.CompletedTask;
        }

        if (!Guid.TryParse(envelope.TenantId, out var tenantId))
        {
            throw new InvalidOperationException($"Envelope has a non-GUID TenantId '{envelope.TenantId}'.");
        }

        currentCompany.Change(new CompanyId(tenantId));
        return Task.CompletedTask;
    }
}
