using AppointMe.Shared.Users;

namespace AppointMe.Identity.Users.RegisterUser;

public static class RegisterUser
{
    extension(User)
    {
        public static User Register(IdentityProviderUserId identityProviderUserId, PersonName name, Email email,
            DateTimeOffset registrationDate)
        {
            var user = new User
            {
                Id = new UserId(NewId()),
                IdentityProviderUserId = identityProviderUserId,
                Name = name,
                Email = email,
                RegisteredAt = registrationDate,
                IsDeleted = false
            };
            user.Raise(new UserRegisteredEvent(user.Id.Value));
            return user;
        }
    }
}
