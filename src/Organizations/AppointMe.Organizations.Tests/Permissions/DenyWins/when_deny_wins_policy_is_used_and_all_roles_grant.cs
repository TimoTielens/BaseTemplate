namespace AppointMe.Organizations.Tests.Permissions.DenyWins;

public class when_deny_wins_policy_is_used_and_all_roles_grant : behaves_like_resolving_permissions
{
    protected override IOverrideConflictPolicy Policy => new DenyWinsPolicy();

    protected override IReadOnlySet<Role> Roles => new HashSet<Role>
    {
        Role.Manager,
        Role.Staff
    };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Manager).WithPermission(Write).Build(),
        Create.Override.WithRole(Role.Staff).WithPermission(Write).Build()
    ];

    [Fact]
    public void should_grant_the_permission()
    {
        Assert.Contains(Write, Result);
    }
}
