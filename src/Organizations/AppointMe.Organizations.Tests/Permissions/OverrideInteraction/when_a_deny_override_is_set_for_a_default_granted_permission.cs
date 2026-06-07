namespace AppointMe.Organizations.Tests.Permissions.OverrideInteraction;

public sealed class when_a_deny_override_is_set_for_a_default_granted_permission : behaves_like_resolving_permissions
{
    // Manager gets Read by default; an explicit deny must strip it
    protected override IReadOnlySet<Role> Roles => new HashSet<Role>
    {
        Role.Manager
    };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override
            .WithRole(Role.Manager)
            .WithPermission(Read).Denied()
            .Build()
    ];

    [Fact]
    public void should_remove_the_permission() => Assert.Empty(Result);
}
