using AppointMe.Organizations.Database;
using Microsoft.Extensions.Caching.Hybrid;

namespace AppointMe.Organizations.Settings.Permissions.Infrastructure;

public sealed class RolePermissionOverridesCache(OrganizationsDbContext dbContext, HybridCache cache)
{
    private static readonly HybridCacheEntryOptions Options = new()
    {
        Expiration = TimeSpan.FromHours(1),
        LocalCacheExpiration = TimeSpan.FromHours(1),
    };

    public ValueTask<IReadOnlyList<RolePermissionOverride>> GetAsync(CompanyId companyId,
        CancellationToken cancellationToken)
    {
        return cache.GetOrCreateAsync(
            KeyFor(companyId),
            (dbContext, companyId),
            static async (state, ct) => (IReadOnlyList<RolePermissionOverride>)await state.dbContext
                .RolePermissionOverrides
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(permissionOverride => permissionOverride.CompanyId == state.companyId)
                .ToListAsync(ct),
            Options,
            cancellationToken: cancellationToken);
    }

    public ValueTask InvalidateAsync(CompanyId companyId, CancellationToken cancellationToken = default)
    {
        return cache.RemoveAsync(KeyFor(companyId), cancellationToken);
    }

    private static string KeyFor(CompanyId companyId) => $"role-permission-overrides:{companyId.Value}";
}
