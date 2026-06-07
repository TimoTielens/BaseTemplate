using System.Globalization;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Identity.Tests.Users;

public sealed class UserBuilder
{
    private readonly FakeTimeProvider _timeProvider =
        new(DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));

    public User Build() => new()
    {
        Id = new UserId(NewId()),
        IdentityProviderUserId = new IdentityProviderUserId("kc-" + NewId().ToString("N")),
        Name = PersonName.Create("Jane", "Doe"),
        Email = Email.Create("jane.doe@example.com"),
        RegisteredAt = _timeProvider.GetUtcNow(),
        IsDeleted = false
    };
}
