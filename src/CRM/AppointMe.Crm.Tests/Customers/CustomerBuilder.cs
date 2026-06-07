using System.Globalization;
using AppointMe.Shared.Companies;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Crm.Tests.Customers;

public sealed class CustomerBuilder
{
    private readonly FakeTimeProvider _timeProvider = new(DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));

    public Customer Build() => new()
    {
        Id = new CustomerId(NewId()),
        CompanyId = new CompanyId(NewId()),
        Name = PersonName.FromFullName("John Doe"),
        DateOfBirth = new DateOfBirth(DateOnly.Parse("2000-02-29", DateTimeFormatInfo.InvariantInfo)),
        Gender = Gender.Male,
        Email = Email.Create("john.doe@gmail.com"),
        RegistrationDate = _timeProvider.GetUtcNow(),
        IsDeleted = false
    };
}
