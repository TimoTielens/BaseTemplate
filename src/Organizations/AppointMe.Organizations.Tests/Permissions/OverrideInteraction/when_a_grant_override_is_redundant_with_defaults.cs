namespace AppointMe.Organizations.Tests.Permissions.OverrideInteraction;

public class when_a_grant_override_is_redundant_with_defaults : behaves_like_resolving_permissions
{
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Manager };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Manager).WithPermission(Read).Build()
    ];

    [Fact]
    public void should_remain_granted() => Assert.Equivalent(new[] { Read }, Result, strict: true);
}
