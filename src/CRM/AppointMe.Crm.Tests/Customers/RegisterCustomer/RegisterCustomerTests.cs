using System.Globalization;
using AppointMe.Crm.Contracts.Customers.Events;
using AppointMe.Crm.Customers.RegisterCustomer;
using AppointMe.Shared.Companies;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Crm.Tests.Customers.RegisterCustomer;

public class RegisterCustomerTests
{
    [Fact]
    public void should_register_customer_and_raise_event()
    {
        var companyId = new CompanyId(Guid.NewGuid());
        var name = PersonName.Create("John", "Doe");
        var email = Email.Create("john@example.com");
        var timeProvider = new FakeTimeProvider(DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));
        var dateOfBirth = DateOfBirth.Create(DateOnly.Parse("2000-01-10", DateTimeFormatInfo.InvariantInfo), timeProvider);
        const Gender gender = Gender.Female;
        var registrationDate = timeProvider.GetUtcNow();

        var customer = Customer.Register(companyId, name, dateOfBirth, gender, email, registrationDate);

        Assert.NotEqual(Guid.Empty, customer.Id.Value);
        Assert.Equal(name, customer.Name);
        Assert.Equal(dateOfBirth, customer.DateOfBirth);
        Assert.Equal(gender, customer.Gender);
        Assert.Equal(email, customer.Email);
        Assert.Equal(registrationDate, customer.RegistrationDate);

        var @event = customer.Events.OfType<CustomerRegisteredEvent>().Single();
        Assert.Equal(customer.Id.Value, @event.CustomerId);
        Assert.Equal(companyId.Value, @event.CompanyId);
        Assert.Equal("John", @event.FirstName);
        Assert.Equal("Doe", @event.LastName);
        Assert.Equal(dateOfBirth.Value, @event.DateOfBirth);
        Assert.Equal(email.Value, @event.Email);
    }
}
