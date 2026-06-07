namespace AppointMe.Organizations.Tests.Permissions.OverrideFiltering;

public class when_an_override_belongs_to_a_non_held_role : behaves_like_resolving_permissions
{
    // Manager is not an active role — its grant override must not bleed in
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Staff };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Manager).WithPermission(Write).Build()
    ];

    [Fact]
    public void should_be_ignored() => Assert.Empty(Result);
}
