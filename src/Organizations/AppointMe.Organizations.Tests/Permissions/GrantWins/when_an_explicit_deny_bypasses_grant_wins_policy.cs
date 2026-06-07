namespace AppointMe.Organizations.Tests.Permissions.GrantWins;

public sealed class when_an_explicit_deny_bypasses_grant_wins_policy : behaves_like_resolving_permissions
{
    protected override IOverrideConflictPolicy Policy => new GrantWinsPolicy();

    protected override IReadOnlySet<Role> Roles => new HashSet<Role>
    {
        Role.Manager,
        Role.Staff
    };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        // We add an explicit Deny for Staff.
        // We DO NOT add an override for Manager, so it relies on its default grant.
        Create.Override.WithRole(Role.Staff).WithPermission(Read).Denied().Build()
    ];

    [Fact]
    public void should_grant_the_permission_because_manager_role_implicitly_grants_it()
    {
        // GrantWinsPolicy preserves the implicit grant from Manager's defaults despite the explicit deny on Staff.
        Assert.Contains(Read, Result);
    }
}
