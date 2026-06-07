using AppointMe.Shared.Authorization.Permissions.DefaultGrants;
using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Shared.Authorization.Permissions;

public sealed class PermissionRegistry
{
    private readonly Dictionary<string, Permission> _permissionsByCode;
    private readonly Dictionary<Role, HashSet<Permission>> _defaultGrants;

    public PermissionRegistry(IEnumerable<Permission> permissions, IEnumerable<IDefaultGrantPolicy> grantPolicies)
    {
        _permissionsByCode = permissions.ToDictionary(permission => permission.Code);

        _defaultGrants = grantPolicies
            .SelectMany(policy => policy.DefaultGrants)
            .GroupBy(permissionGrant => permissionGrant.Role)
            .ToDictionary(
                group => group.Key,
                group => group.SelectMany(x => x.Permissions).ToHashSet());
    }

    public bool Contains(string code) => _permissionsByCode.ContainsKey(code);

    public Permission Get(string code) => _permissionsByCode.TryGetValue(code, out var permission)
        ? permission
        : throw new InvalidOperationException($"Unknown permission '{code}'");

    public IReadOnlySet<Permission> GetDefaultGrants(Role role) =>
        _defaultGrants.TryGetValue(role, out var grants) ? grants : [];

    public bool IsGrantedByDefault(Role role, string permissionCode) =>
        _defaultGrants.TryGetValue(role, out var grants) && grants.Any(permission => permission.Code == permissionCode);

    public IReadOnlyCollection<Permission> Permissions => _permissionsByCode.Values;
}
