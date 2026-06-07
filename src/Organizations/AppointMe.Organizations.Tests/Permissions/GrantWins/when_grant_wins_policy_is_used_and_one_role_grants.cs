namespace AppointMe.Organizations.Tests.Permissions.GrantWins;

public sealed class when_grant_wins_policy_is_used_and_one_role_grants : behaves_like_resolving_permissions
{
    protected override IOverrideConflictPolicy Policy => new GrantWinsPolicy();

    protected override IReadOnlySet<Role> Roles => new HashSet<Role>
    {
        Role.Manager,
        Role.Staff
    };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Manager).WithPermission(Write).Build(),
        Create.Override.WithRole(Role.Staff).WithPermission(Write).Denied().Build()
    ];

    [Fact]
    public void should_grant_the_permission()
    {
        Assert.Contains(Write, Result);
    }
}
