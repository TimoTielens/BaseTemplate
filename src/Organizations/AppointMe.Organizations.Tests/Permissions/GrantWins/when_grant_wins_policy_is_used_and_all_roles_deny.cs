namespace AppointMe.Organizations.Tests.Permissions.GrantWins;

// Manager has Read by default; explicit deny from both roles must override it
public sealed class when_grant_wins_policy_is_used_and_all_roles_deny : behaves_like_resolving_permissions
{
    protected override IOverrideConflictPolicy Policy => new GrantWinsPolicy();

    protected override IReadOnlySet<Role> Roles => new HashSet<Role>
    {
        Role.Manager,
        Role.Staff
    };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Manager).WithPermission(Read).Denied().Build(),
        Create.Override.WithRole(Role.Staff).WithPermission(Read).Denied().Build()
    ];

    [Fact]
    public void should_deny_the_permission()
    {
        Assert.DoesNotContain(Read, Result);
    }
}
