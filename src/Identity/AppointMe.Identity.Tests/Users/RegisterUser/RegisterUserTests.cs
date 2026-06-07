using System.Globalization;
using AppointMe.Identity.Users.RegisterUser;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Identity.Tests.Users.RegisterUser;

public class RegisterUserTests
{
    [Fact]
    public void should_register_user_with_initial_state_and_raise_event()
    {
        var identityProviderUserId = new IdentityProviderUserId("kc-12345");
        var name = PersonName.Create("Jane", "Doe");
        var email = Email.Create("jane.doe@example.com");
        var timeProvider = new FakeTimeProvider(
            DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));
        var registrationDate = timeProvider.GetUtcNow();

        var user = User.Register(identityProviderUserId, name, email, registrationDate);

        Assert.NotEqual(Guid.Empty, user.Id.Value);
        Assert.Equal(identityProviderUserId, user.IdentityProviderUserId);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(registrationDate, user.RegisteredAt);
        Assert.False(user.IsDeleted);

        var @event = user.Events.OfType<UserRegisteredEvent>().Single();
        Assert.Equal(user.Id.Value, @event.UserId);
    }
}
