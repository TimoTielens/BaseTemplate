using AppointMe.Organizations.Contracts.Settings.Permissions.Events;
using AppointMe.Shared.Authorization.Permissions;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Settings.Permissions.UpdatePermissions;

public static class UpdatePermissions
{
    extension(CompanyPermissionPolicy policy)
    {
        public CompanyPermissionsChanged? ApplyGrants(IReadOnlyList<RolePermissionGrantDto> grants,
            PermissionRegistry registry, UserId? changedBy)
        {
            ValidateGrants(grants, registry);

            var uniqueGrants = grants
                .GroupBy(grant => new { grant.Role, grant.PermissionKey })
                .Select(grouping => grouping.Last())
                .ToList();

            var anyChange = false;
            foreach (var grant in uniqueGrants)
            {
                var defaultGranted = registry.IsGrantedByDefault(grant.Role, grant.PermissionKey);
                policy.Overrides.TryGetValue((grant.PermissionKey, grant.Role), out var existing);

                var action = PermissionGrantDecision.Decide(grant.IsGranted, defaultGranted, existing?.IsGranted);
                anyChange |= policy.Apply(action, grant, existing);
            }

            return anyChange
                ? new CompanyPermissionsChanged(policy.CompanyId.Value, changedBy?.Value)
                : null;
        }

        private bool Apply(PermissionGrantAction action, RolePermissionGrantDto grant,
            RolePermissionOverride? existing)
        {
            switch (action)
            {
                case PermissionGrantAction.Add:
                    var added = new RolePermissionOverride
                    {
                        CompanyId = policy.CompanyId,
                        PermissionCode = grant.PermissionKey,
                        Role = grant.Role,
                        IsGranted = grant.IsGranted,
                    };
                    policy.Overrides[(grant.PermissionKey, grant.Role)] = added;
                    policy.Added.Add(added);
                    return true;

                case PermissionGrantAction.Update when existing is not null:
                    existing.IsGranted = grant.IsGranted;
                    return true;

                case PermissionGrantAction.Remove when existing is not null:
                    policy.Overrides.Remove((grant.PermissionKey, grant.Role));
                    policy.Removed.Add(existing);
                    return true;

                case PermissionGrantAction.NoOp:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }

    private static void ValidateGrants(IReadOnlyList<RolePermissionGrantDto> grants, PermissionRegistry registry)
    {
        var maximumGrants = registry.Permissions.Count * Role.Configurable.Count;
        if (grants.Count > maximumGrants)
        {
            throw new ValidationException($"At most {maximumGrants} grants can be modified.",
                code: "too_many_grants");
        }

        foreach (var grant in grants)
        {
            if (!registry.Contains(grant.PermissionKey))
            {
                throw new ValidationException($"Unknown permission key: '{grant.PermissionKey}'",
                    code: "unknown_permission");
            }

            if (registry.Get(grant.PermissionKey) is SystemPermission)
            {
                throw new ValidationException(
                    $"Permission '{grant.PermissionKey}' is system-managed and cannot be overridden.",
                    code: "system_managed_permission");
            }

            if (grant.Role is SystemRole)
            {
                throw new ValidationException($"Permissions for the {grant.Role.Name} role cannot be modified.",
                    code: "role_permissions_immutable");
            }
        }
    }
}
