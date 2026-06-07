namespace AppointMe.Organizations.Tests.Permissions;

public abstract class behaves_like_resolving_permissions : Specification
{
    protected static readonly Permission Read = new("test", "read");
    protected static readonly Permission Write = new("test", "write");
    protected static readonly Permission Delete = new("test", "delete");

    private static readonly PermissionRegistry Registry = new(
        permissions: [Read, Write, Delete],
        grantPolicies:
        [
            new TestDefaultGrants(
                new RolePermissionGrant(Role.Owner, Read, Write, Delete),
                new RolePermissionGrant(Role.Manager, Read)
            )
        ]);

    protected IReadOnlySet<Permission> Result = null!;

    protected virtual IReadOnlySet<Role> Roles => new HashSet<Role>();
    protected virtual IEnumerable<RolePermissionOverride> Overrides => [];
    protected virtual IOverrideConflictPolicy Policy => new DenyWinsPolicy();

    protected override void Because()
    {
        Result = new PermissionResolver(Registry, Policy).Resolve(Roles, Overrides);
    }

    private sealed class TestDefaultGrants(params IEnumerable<RolePermissionGrant> defaultGrants) : IDefaultGrantPolicy
    {
        public IReadOnlyCollection<RolePermissionGrant> DefaultGrants => defaultGrants.ToArray();
    }
}
