using AppointMe.Organizations.Companies;
using AppointMe.Organizations.Employees;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Tests.Companies;

public class CompanyOwnershipTests
{
    private static Company CompanyWithPrimaryOwner(EmployeeId primaryOwnerId) => new()
    {
        Id = new CompanyId(Guid.NewGuid()),
        Name = new CompanyName("Acme"),
        TimeZone = TimeZoneInfo.Utc,
        RegisteredBy = new UserId(Guid.NewGuid()),
        RegistrationDate = DateTimeOffset.UnixEpoch,
        PrimaryOwnerEmployeeId = primaryOwnerId,
    };

    [Fact]
    public void should_lock_owner_role_for_the_primary_owner()
    {
        var primaryOwnerId = new EmployeeId(Guid.NewGuid());
        var company = CompanyWithPrimaryOwner(primaryOwnerId);

        Assert.True(company.IsPrimaryOwner(primaryOwnerId));
        Assert.Equivalent(new[] { Role.Owner }, company.LockedRolesFor(primaryOwnerId), strict: true);
    }

    [Fact]
    public void should_lock_no_roles_for_a_non_primary_owner()
    {
        var company = CompanyWithPrimaryOwner(new EmployeeId(Guid.NewGuid()));
        var otherId = new EmployeeId(Guid.NewGuid());

        Assert.False(company.IsPrimaryOwner(otherId));
        Assert.Empty(company.LockedRolesFor(otherId));
    }
}
