namespace AppointMe.Organizations.Tests.Permissions.OverrideFiltering;

public class when_a_non_held_role_grant_override_competes_with_a_held_role_deny_override : behaves_like_resolving_permissions
{
    // Owner (not held) grants Write — even with a permissive GrantWins policy
    // it must not offset the held Manager's deny
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Manager };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Owner).WithPermission(Write).Build(), // Owner not held
        Create.Override.WithRole(Role.Manager).WithPermission(Write).Denied().Build() // Manager held, denies
    ];

    protected override IOverrideConflictPolicy Policy => new GrantWinsPolicy();

    [Fact]
    public void should_not_count_the_non_held_role_vote() => Assert.DoesNotContain(Write, Result);
}
