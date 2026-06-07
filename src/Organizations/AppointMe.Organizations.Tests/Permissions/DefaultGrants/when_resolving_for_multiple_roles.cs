namespace AppointMe.Organizations.Tests.Permissions.DefaultGrants;

public class when_resolving_for_multiple_roles : behaves_like_resolving_permissions
{
    // Manager adds Read, Owner adds Read+Write+Delete — union should cover all three
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Owner, Role.Manager };

    [Fact]
    public void should_union_the_default_grants() =>
        Assert.Equivalent(new[] { Read, Write, Delete }, Result, strict: true);
}
