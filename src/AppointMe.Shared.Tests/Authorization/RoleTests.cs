using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Shared.Tests.Authorization;

public class RoleTests
{
    [Fact]
    public void should_return_canonical_SystemRole_when_creating_owner_by_name()
    {
        var role = Role.Create("Owner");

        Assert.IsType<SystemRole>(role);
        Assert.Same(Role.Owner, role);
    }

    [Theory]
    [InlineData("Manager")]
    [InlineData("Staff")]
    [InlineData("Receptionist")]
    public void should_return_canonical_built_in_when_creating_by_name(string name)
    {
        var role = Role.Create(name);

        Assert.Contains(role, Role.BuiltIn);
        Assert.IsNotType<SystemRole>(role);
        Assert.Equal(name, role.Name);
    }

    [Fact]
    public void should_return_plain_Role_when_creating_with_unknown_name()
    {
        var role = Role.Create("CustomTenantRole");

        Assert.Equal(typeof(Role), role.GetType());
        Assert.Equal("CustomTenantRole", role.Name);
    }

    [Fact]
    public void should_throw_when_creating_with_empty_name()
    {
        var error = Assert.Throws<ValidationException>(() => Role.Create(""));

        Assert.Equal("role_name_empty", error.Code);
    }

    [Fact]
    public void should_throw_when_creating_with_name_exceeding_max_length()
    {
        var tooLong = new string('x', Role.MaxLength + 1);

        var error = Assert.Throws<ValidationException>(() => Role.Create(tooLong));

        Assert.Equal("role_name_too_long", error.Code);
    }
}
