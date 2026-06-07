using AppointMe.Organizations.Settings.Permissions;
using AppointMe.Shared.Authorization.Permissions;
using AppointMe.Shared.Authorization.Permissions.OverrideConflicts;

namespace AppointMe.Organizations.Infrastructure;

public sealed class PermissionResolver(PermissionRegistry registry, IOverrideConflictPolicy conflictPolicy)
{
    public IReadOnlySet<Permission> Resolve(IReadOnlySet<Role> roles, IEnumerable<RolePermissionOverride> overrides)
    {
        var effective = new HashSet<Permission>();

        // Apply default grants
        foreach (var role in roles)
        {
            effective.UnionWith(registry.GetDefaultGrants(role));
        }

        // Apply overrides — group by permission so the conflict policy can arbitrate
        // when multiple roles disagree on the same permission.
        var overridesByPermission = overrides
            .Where(permissionOverride => roles.Contains(permissionOverride.Role))
            .GroupBy(permissionOverride => permissionOverride.PermissionCode);

        foreach (var permissionGroup in overridesByPermission)
        {
            var permission = registry.Get(permissionGroup.Key);
            var overridesByRole = permissionGroup.ToDictionary(
                permissionOverride => permissionOverride.Role,
                permissionOverride => permissionOverride.IsGranted);

            var votes = new List<PermissionVote>();
            foreach (var role in roles)
            {
                if (overridesByRole.TryGetValue(role, out var explicitGrant))
                {
                    votes.Add(new PermissionVote(role, explicitGrant));
                }
                else if (registry.GetDefaultGrants(role).Contains(permission))
                {
                    votes.Add(new PermissionVote(role, true));
                }
            }

            if (votes.Count == 0)
            {
                continue;
            }

            var isGranted = conflictPolicy.Resolve(votes);
            if (isGranted)
            {
                effective.Add(permission);
            }
            else
            {
                effective.Remove(permission);
            }
        }

        return effective;
    }
}
